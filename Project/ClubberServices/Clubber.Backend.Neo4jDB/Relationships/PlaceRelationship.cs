using Neo4jClient;

namespace Clubber.Backend.Neo4jDB.Relationships
{
    public class PlaceRelationship : Relationship, IRelationshipAllowingSourceNode<string>, IRelationshipAllowingTargetNode<string>
    {
        public static readonly string TypeKey = "PLACE";

        /// <summary>
        /// Relationship: Event->[place]->Club
        /// </summary>
        public PlaceRelationship(NodeReference targetNode) : base(targetNode) { }

        public override string RelationshipTypeKey
        {
            get
            {
                return TypeKey;
            }
        }
    }
}