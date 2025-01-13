/*
    Resyslib
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Threading.Tasks;

using Resyslib.Runtime.Abstractions;

namespace Resyslib.Runtime
{
    public class PlatformInformation
    {
        public static async Task<Platform> GetPlatformAsync(IPlatformProviderFactory platformProviderFactory)
        {
            IPlatformProvider provider = platformProviderFactory.CreatePlatformProvider();

            return await provider.GetCurrentPlatformAsync();
        }
        
        public static async Task<bool> IsPlatformAsync(Platform platform)
        {
            return await IsPlatformAsync(platform, new DefaultPlatformProviderFactory());
        }

        public static async Task<bool> IsPlatformAsync(Platform platform, IPlatformProviderFactory platformProviderFactory)
        {
            bool success = platformProviderFactory.TryCreatePlatformProvider(platform.Family, out IPlatformProvider? platformProvider);

            if (success && platformProvider != null)
            {
                Platform currentPlatform = await platformProvider.GetCurrentPlatformAsync();
                
                return platform.Equals(currentPlatform);
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        public static async Task<bool> IsPlatformFamilyAsync(PlatformFamily family)
        {
            return await IsPlatformFamilyAsync(family, new DefaultPlatformProviderFactory());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="family"></param>
        /// <param name="platformProviderSelector"></param>
        /// <returns></returns>
        public static async Task<bool> IsPlatformFamilyAsync(PlatformFamily family,
            IPlatformProviderFactory platformProviderFactory)
        {
            bool success = platformProviderFactory.TryCreatePlatformProvider(family, out IPlatformProvider? platformProvider);

            if (success && platformProvider != null)
            {
                Platform currentPlatform = await platformProvider.GetCurrentPlatformAsync();
                
                return family.Equals(currentPlatform.Family);
            }
            else
            {
                return false;
            }
        }
    }
}