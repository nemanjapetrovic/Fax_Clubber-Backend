using System.Collections.Generic;

namespace Clubber.Backend.Services.Logic.RelationshipServices
{
    public interface IRelationshipServices
    {
        void CreateRelationship(string relationshipKey,
            string beginNodeLabel,
            string endNodeLabel,
            string beginNodeId,
            string endNodeId);

        void RemoveRelationship(string relationshipKey,
            string beginNodeLabel,
            string endNodeLabel,
            string beginNodeId,
            string endNodeId);

        IList<string> GetElementsInRelationshipWith(string relationshipKey,
            string beginNodeLabel,
            string endNodeLabel,
            string beginNodeId);
    }
}
