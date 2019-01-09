using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colso.Xrm.WebResourceAutoUpdater.AppCode
{
    public static class ResourceUpdater
    {
        public static Guid[] Upload(this IOrganizationService service, string basefolder, string[] files)
        {
            var pubResourceList = new List<Guid>();

            foreach (var f in files.OrderBy(f => f))
            {
                var name = string.Concat(f.Replace(basefolder, string.Empty));
                name = name.Trim('\\').Replace("\\", "/");

                var q = new QueryExpression("webresource");
                q.ColumnSet = new ColumnSet("name");
                q.Criteria.AddCondition("name", ConditionOperator.Equal, name);

                var wr = service.RetrieveMultiple(q).Entities.FirstOrDefault();

                if (wr != null)
                {
                    wr["content"] = Convert.ToBase64String(File.ReadAllBytes(f));
                    service.Update(wr);
                    pubResourceList.Add(wr.Id);
                }
                else
                {
                    // IF NOT EXISTS CREATE?
                    var wrNew = new Entity("webresource");
                    wrNew["name"] = name;
                    wrNew["content"] = Convert.ToBase64String(File.ReadAllBytes(f));

                    var wrType = new OptionSetValue();

                    switch (Path.GetExtension(f).ToLower())
                    {
                        case ".js":
                            wrType.Value = 3;
                            break;
                        case ".png":
                        case ".jpg":
                        case ".ico":
                        case ".gif":
                        case ".jpeg":
                            wrType.Value = 5;
                            break;
                        case ".html":
                        case ".htm":
                            wrType.Value = 1;
                            break;
                        case ".xap":
                            wrType.Value = 8;
                            break;
                        default:
                            wrType.Value = 2;
                            break;
                    }
                    wrNew["webresourcetype"] = wrType;

                    pubResourceList.Add(service.Create(wrNew));
                }
            }

            return pubResourceList.ToArray();
        }

        public static void Publish(this IOrganizationService service, Guid[] resourceIds)
        {
            var publishXml = string.Concat("<importexportxml><webresources>", string.Join(string.Empty, resourceIds.Select(id => string.Format("<webresource>{0}</webresource>", id.ToString())).ToArray()), "</webresources></importexportxml>");

            var publishReq = new PublishXmlRequest() { ParameterXml = publishXml };
            service.Execute(publishReq);
        }

        public static void AddToSolution(this IOrganizationService service, string solutionUniqueName, Guid[] resourceIds)
        {
            foreach (var id in resourceIds)
            {
                var solutionAddReq = new AddSolutionComponentRequest() { ComponentId = id, ComponentType = 61, SolutionUniqueName = solutionUniqueName };
                service.Execute(solutionAddReq);
            }
        }
        public static Entity[] GetUnmanagedSolutions(this IOrganizationService service)
        {
            var q = new QueryExpression("solution");
            q.ColumnSet = new ColumnSet("friendlyname", "uniquename");
            q.Criteria.AddCondition("ismanaged", ConditionOperator.Equal, false);

            return service.RetrieveMultiple(q).Entities
                .OrderBy(s => s.GetAttributeValue<string>("friendlyname"))
                .ToArray();
        }
    }
}

