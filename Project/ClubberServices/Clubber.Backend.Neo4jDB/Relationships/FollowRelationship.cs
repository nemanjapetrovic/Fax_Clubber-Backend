using Neo4jClient;

namespace Clubber.Backend.Neo4jDB.Relationships
{
    public class FollowRelationship : Relationship, IRelationshipAllowingSourceNode<string>, IRelationshipAllowingTargetNode<string>
    {
        public static readonly string TypeKey = "FOLLOW";

        /// <summary>
        /// Relationship: User->[follow]->Club
        /// </summary>
        public FollowRelationship(NodeReference targetNode) : base(targetNode) { }

        public override string RelationshipTypeKey
        {
            get
            {
                return TypeKey;
            }
        }
    }
}