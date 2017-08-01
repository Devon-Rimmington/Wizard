//  Copyright 2017 MaterialUI for Unity http://materialunity.com
//  Please see license file for terms and conditions of use, and more information.

using UnityEngine;
using System;
using System.Collections.Generic;

namespace MaterialUI
{
    public class VectorImageParserIonicons : VectorImageFontParser
    {
        protected override string GetIconFontUrl()
        {
            return "https://github.com/driftyco/ionicons/blob/master/fonts/ionicons.ttf?raw=true";
        }

        protected override string GetIconFontLicenseUrl()
        {
            return "https://github.com/driftyco/ionicons/blob/master/LICENSE?raw=true";
        }

        protected override string GetIconFontDataUrl()
        {
            return "https://raw.githubusercontent.com/driftyco/ionicons/master/builder/manifest.json?raw=true";
        }

        public override string GetWebsite()
        {
            return "http://ionicons.com/";
        }

        public override string GetFontName()
        {
            return "Ionicons";
        }

        protected override VectorImageSet GenerateIconSet(string fontDataContent)
        {
            VectorImageSet vectorImageSet = new VectorImageSet();

            fontDataContent = fontDataContent.Replace("name", "m_Name").Replace("code", "m_Unicode").Replace("icons", "m_IconGlyphList");

            VectorImageSet ioniconsInfo = JsonUtility.FromJson<VectorImageSet>(fontDataContent);

            for (int i = 0; i < ioniconsInfo.iconGlyphList.Count; i++)
            {
                string name = ioniconsInfo.iconGlyphList[i].name;
                string unicode = ioniconsInfo.iconGlyphList[i].unicode;
                unicode = unicode.Replace("0x", string.Empty);

                vectorImageSet.iconGlyphList.Add(new Glyph(name, unicode, false));
            }

            return vectorImageSet;
        }

        protected override string ExtractLicense(string fontDataLicenseContent)
        {
            return fontDataLicenseContent;
        }
    }
}
