using Neo4jClient;

namespace Clubber.Backend.Neo4jDB.Relationships
{
    public class ManageRelationship : Relationship, IRelationshipAllowingSourceNode<string>, IRelationshipAllowingTargetNode<string>
    {
        public static readonly string TypeKey = "MANAGE";

        /// <summary>
        /// Relationship: Manager->[manage]->Club
        /// </summary>
        public ManageRelationship(NodeReference targetNode) : base(targetNode) { }

        public override string RelationshipTypeKey
        {
            get
            {
                return TypeKey;
            }
        }
    }
}