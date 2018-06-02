using System;

namespace Entites.Contracts
{
    public struct UserId : IEquatable<UserId>
    {
        #region Constructors

        public UserId(int id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public int Id { get; }

        #endregion

        #region Operators

        public static bool operator ==(UserId left, UserId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(UserId left, UserId right)
        {
            return !left.Equals(right);
        }

        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is UserId && Equals((UserId)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        #endregion

        #region IEquatable<UserId> Members

        public bool Equals(UserId other)
        {
            return Id == other.Id;
        }

        #endregion
    }
}
