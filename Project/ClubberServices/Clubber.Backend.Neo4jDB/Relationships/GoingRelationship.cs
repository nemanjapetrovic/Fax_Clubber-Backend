using Neo4jClient;

namespace Clubber.Backend.Neo4jDB.Relationships
{
    public class GoingRelationship : Relationship, IRelationshipAllowingSourceNode<string>, IRelationshipAllowingTargetNode<string>
    {
        public static readonly string TypeKey = "GOING";

        /// <summary>
        /// Relationship: User->[going]->Event
        /// </summary>
        public GoingRelationship(NodeReference targetNode) : base(targetNode) { }

        public override string RelationshipTypeKey
        {
            get
            {
                return TypeKey;
            }
        }
    }
}