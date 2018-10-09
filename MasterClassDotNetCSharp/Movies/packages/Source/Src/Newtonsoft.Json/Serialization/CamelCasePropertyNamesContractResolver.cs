#region License
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
    internal readonly struct ResolverContractKey : IEquatable<ResolverContractKey>
    {
        private readonly Type _resolverType;
        private readonly Type _contractType;

        public ResolverContractKey(Type resolverType, Type contractType)
        {
            _resolverType = resolverType;
            _contractType = contractType;
        }

        public override int GetHashCode()
        {
            return _resolverType.GetHashCode() ^ _contractType.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ResolverContractKey))
            {
                return false;
            }

            return Equals((ResolverContractKey)obj);
        }

        public bool Equals(ResolverContractKey other)
        {
            return (_resolverType == other._resolverType && _contractType == other._contractType);
        }
    }

    /// <summary>
    /// Resolves member mappings for a type, camel casing property names.
    /// </summary>
    public class CamelCasePropertyNamesContractResolver : DefaultContractResolver
    {
        private static readonly object TypeContractCacheLock = new object();
        private static readonly PropertyNameTable NameTable = new PropertyNameTable();
        private static Dictionary<ResolverContractKey, JsonContract> _contractCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="CamelCasePropertyNamesContractResolver"/> class.
        /// </summary>
        public CamelCasePropertyNamesContractResolver()
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                ProcessDictionaryKeys = true,
                OverrideSpecifiedNames = true
            };
        }

        /// <summary>
        /// Resolves the contract for a given type.
        /// </summary>
        /// <param name="type">The type to resolve a contract for.</param>
        /// <returns>The contract for a given type.</returns>
        public override JsonContract ResolveContract(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            // for backwards compadibility the CamelCasePropertyNamesContractResolver shares contracts between instances
            JsonContract contract;
            ResolverContractKey key = new ResolverContractKey(GetType(), type);
            Dictionary<ResolverContractKey, JsonContract> cache = _contractCache;
            if (cache == null || !cache.TryGetValue(key, out contract))
            {
                contract = CreateContract(type);

                // avoid the possibility of modifying the cache dictionary while another thread is accessing it
                lock (TypeContractCacheLock)
                {
                    cache = _contractCache;
                    Dictionary<ResolverContractKey, JsonContract> updatedCache = (cache != null)
                        ? new Dictionary<ResolverContractKey, JsonContract>(cache)
                        : new Dictionary<ResolverContractKey, JsonContract>();
                    updatedCache[key] = contract;

                    _contractCache = updatedCache;
                }
            }

            return contract;
        }

        internal override PropertyNameTable GetNameTable()
        {
            return NameTable;
        }
    }
}