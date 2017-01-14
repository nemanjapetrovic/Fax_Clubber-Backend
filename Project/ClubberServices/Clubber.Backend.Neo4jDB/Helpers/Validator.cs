namespace Clubber.Backend.Neo4jDB.Helpers
{
    internal static class Validator
    {
        /// <summary>
        /// Used to validate the type of Relationship.
        /// </summary>
        internal static bool ValidateRelationshipsNames(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            switch (value)
            {
                case Constants.Relationships.FollowRelationship:
                    return true;
                case Constants.Relationships.GoingRelationship:
                    return true;
                case Constants.Relationships.ManageRelationship:
                    return true;
                case Constants.Relationships.PlaceRelationship:
                    return true;
                default: return false;
            }
        }

        /// <summary>
        /// Used to validate the type of Node Labels.
        /// </summary>
        internal static bool ValidateNodeLabelsNames(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            switch (value)
            {
                default: return false;
            }
        }
    }
}
