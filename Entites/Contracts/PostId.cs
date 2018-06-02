using System;

namespace Entites.Contracts
{
    public struct PostId : IEquatable<PostId>
    {
        #region Constructors

        public PostId(int id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public int Id { get; }

        #endregion

        #region Operators

        public static bool operator ==(PostId left, PostId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PostId left, PostId right)
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
            return obj is PostId && Equals((PostId)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        #endregion

        #region IEquatable<PostId> Members

        public bool Equals(PostId other)
        {
            return Id == other.Id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        #endregion
    }
}
