﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegIt.Text.StegoTools.SemanticMethod
{
    internal class SemanticLettersValues
    {
        public static Dictionary<char, char> GetLetters()
        {
            return new Dictionary<char, char>
            {
                {'a', 'ɑ'},     //A a À Á Â Ã Ä Å à á â ã ä å ɑ Α α а Ꭺ Ａ ａ 𝚊 𝚊 ɑ а 𝖺 
                {'b', 'Ꮟ'},     //B b ß ʙ Β β В Ь Ᏼ ᛒ Ｂ ｂ𝑏 𝗯 b Ƅ 𝕓 𝖇 Ь 𝓫 𝚋 Ꮟ ᖯ 𝔟 𝒃 𝘣 𝒷 𝙗 𝐛 𝖻
                {'c', 'ᴄ'},     //C c ϲ Ϲ С с Ꮯ Ⅽ ⅽ 𐐠𺀠Ｃ ｃ 𝑐 𝗰 с c ｃ 𝕔 ᴄ ⲥ 𐐽 𝖈 𝓬 𝚌 𝔠 ϲ 𝒄 𝘤 𝒸 𝙘 𝐜 𝖼 ⅽ
                {'ć', 'ċ'},     //č 𝑐́ 𝑐॔ 𝑐݇ 𝑐֜ 𝑐֝ 𝗰́ 𝗰॔ 𝗰݇ 𝗰֜ 𝗰֝ с́ с॔ с݇ с֜ с֝ ć c॔ c݇ c֜ c֝ ｃ́ ｃ॔ ｃ݇ ｃ֜ ｃ֝ 𝕔́ 𝕔॔ 𝕔݇ 𝕔֜ 𝕔֝ ᴄ́ ᴄ॔ ᴄ݇ ᴄ֜ ᴄ֝ ⲥ́ ⲥ॔
                {'d', 'Ꮷ'},     //D d Ď ď Đ đ ԁ ժ Ꭰ Ⅾ ⅾ Ｄ ｄ ԁ 𝓭 𝚍 d ⅆ 𝑑 𝗱 Ꮷ 𝒅 𝘥 𝖉 ᑯ ꓒ 𝐝 𝖽 𝔡 𝕕 ⅾ 𝒹 𝙙
                {'e', 'ė'},     //e  é ê ë  ē  ĕ  ė ě  е ｅ𝓮 𝚎 e ｅ 𝑒 𝗲 ⅇ 𝒆 𝘦 ℮ 𝖊 ℯ 𝐞 𝖾 ꬲ е 𝔢 𝕖 ҽ 𝙚
                {'ę', 'ȩ'},     //𝓮̢ 𝓮ͅ 𝓮᪷ 𝓮̨ 𝚎̢ 𝚎ͅ 𝚎᪷ 𝚎̨ e̢ eͅ e᪷ ę ｅ̢ ｅͅ ｅ᪷ ｅ̨ 𝑒̢ 𝑒ͅ 𝑒᪷ 𝑒̨ 𝗲̢ 𝗲ͅ 𝗲᪷ 𝗲̨ ⅇ̢ ⅇͅ ⅇ᪷ ⅇ̨ 𝒆̢ 𝒆ͅ 𝒆᪷ 𝒆̨ 𝘦̢ 𝘦ͅ
                {'f', 'ƒ'},     //F f Ϝ Ｆｆ𝓯 𝚏 ք 𝑓 𝗳 f 𝒇 𝘧 𝖋 𝐟 𝖿 𝔣 ꬵ 𝕗 ꞙ 𝒻 𝙛 ẝ ſ
                {'g', 'ɡ'},     //ģ ġ  g ɡ ɢ Ԍ ն ｇ𝓰 𝚐 ɡ ց ᶃ 𝑔 𝗴 g ｇ 𝒈 𝘨 ℊ 𝖌 ƍ 𝐠 𝗀 𝔤 𝕘 𝙜
                {'h', 'ｈ'},    //H h ʜ Η Н һ Ꮋ Ｈ ｈᏂ 𝖍 𝓱 𝚑 h ｈ 𝔥 ℎ 𝒉 𝘩 հ 𝒽 𝙝 𝐡 𝗁 𝗵 һ 𝕙
                {'i', 'í'},     //ī î ì ĭ I i l ɩ і ا𝓲 𝞲 ꙇ ⅈ ｉ 𝔦 𝘪 ӏ 𝙞 𝚤 𝐢 і 𝑖 ˛ 𝕚 𝖎 Ꭵ 𝚒 𑣃 i ɩ ɪ 𝒊 𝛊 ⅰ ı 𝒾 𝜾 ⍳ 𝜄 𝗂 𝝸 ℹ ι 𝗶 ͺ
                {'j', 'ĵ'},     //J j ϳ Ј ј յ Ꭻ Ｊ ｊ 𝓳 𝚓 ⅉ 𝔧 j ｊ 𝒋 𝘫 𝒿 𝙟 ϳ 𝐣 𝗃 ј 𝑗 𝗷 𝕛
                {'k', 'ķ'},     //ĸ K k Κ κ К Ꮶ ᛕ K Ｋ ｋ 𝞳 𝔨 ᴋ 𝘬 𝙠 𝛞 𝐤 ⲕ 𝑘 𝜘 𝕜 𝖐 𝚔 𝝒 𝟆 k 𝜅 𝒌 𝞌 𝛋 𝓀 ϰ 𝜿 𝗄 𝗸 ĸ κ к 𝝹
                {'l', 'ĺ'},     //ļ L l ʟ ι ا Ꮮ Ⅼ ⅼ Ｌ ｌ 𝛪 ⵏ 𝝞 𝕝 𝟣 ו 𞣇 𝙡 𝓘 𝗜 𝟙 𝑙 ן Ⅰ 𝘐 ١ 𝒍 𝖑 ￨ 𝐈 l ۱ ꓲ 𝙸 𝟷 𝓵 | ⅼ 𝗹
                {'m', 'ⅿ'},    //M m Μ Ϻ М Ꮇ ᛖ Ⅿ ⅿ Ｍ ｍ 𝒎 𝘮 𑣣 𝖒 𝐦 𝗆 m ᴍ ʍ 𝔪 ꭑ 𝕞 𝓂 𝙢 𝓶 𝚖 rn м 𝑚 𝗺 ⅿ
                {'n', 'ŋ'},     //ŉ ņ N n ɴ Ν Ｎ ｎπ 𝘯 𝐧 𝔫 𝕟 𝙣 ϖ 𝛡 𝚗 𝝕 𝑛 𝜛 𝒏 𝞏 𝖓 𝛑 ᴨ 𝗇 𝝅 𝜋 n 𝟉 𝝿 𝓃 ո 𝓷 ℼ ռ 𝗻 𝞹 п
                {'ń', 'ň'},     //ñ 𝘯॔ 𝘯݇ 𝘯֜ 𝘯֝ 𝐧́ 𝐧॔ 𝐧݇ 𝐧֜ 𝐧֝ 𝔫́
                {'o', 'ｏ'},    //0 O o Ο ο О о Օ 𐐠𱠠Ｏ ｏ  𝞸 𝞼 ꬽ о ھ ο ၀ ہ σ ه ｏ ๐ ໐ 𝕠 𐐬 𞸤 𝙤 ە 𝑜 𝒐 ס 𝜎 𝖔 ٥ 
                {'ó', 'ò'},     //ô õ օ֜ օ֝ 𝐨́ 𝐨॔ 𝐨݇ 𝐨֜ 𝐨֝
                {'p', 'ρ'},     //P p Ρ ρ Р р Ꮲ Ｐ ｐ 𝑝 𝕡 𝖕 𝜚 𝚙 𝞎 ⲣ 𝝔 𝛒 𝒑 𝟈 𝝆 𝓅 𝜌 𝗉 p 𝞀 ϱ 𝗽 ⍴ 
                {'r', 'ŗ'},     //ř R r ʀ Ի Ꮢ ᚱ Ｒ ｒ𝔯 𝒓 𝘳 ⲅ ᴦ ꭇ ꭈ 𝓇 𝙧 𝐫 𝗋 𝑟 𝗿 r г 𝕣 𝖗 𝓻 𝚛
                {'s', 'ｓ'},    //S s Ѕ ѕ Տ Ⴝ Ꮪ 𐐠𵠠Ｓ ｓ𝔰 𑣁 𝒔 𝘴 𝓈 𝙨 𝐬 𝗌 𝑠 𝘀 ꜱ s ｓ 𝕤 ѕ 𝖘 𝓼 𝚜 𐑈 ƽ
                {'ś', 'š'},     //S s Ѕ ѕ Տ Ⴝ Ꮪ 𐐠𵠠Ｓ ｓ
                {'t', 'ţ'},     //T t Τ τ Т Ꭲ Ｔ ｔ𝜏 т 𝐭 𝗍 τ 𝔱 𝕥 𝓉 𝙩 𝝉 𝓽 𝚝 𝞽 t 𝞃 𝑡 𝘁 𝒕 𝘵 ᴛ 𝛕 𝖙
                {'u', 'υ'},     //ù ú ŭ U u μ υ Ա Ս ⋃ Ｕ ｕ𝐮 υ 𝔲 ц 𑣘 𝕦 ʋ 𝙪 ꭎ 𝚞 ꭒ 𝑢 𝒖 𝛖 ᴜ 𝖚 ꞟ 𝜐 𝗎 𝓊 𝝊 𝓾 𝞾 𝞄 u 𝘂 𝘶 ս
                {'w', 'ŵ'},     //W w ѡ Ꮃ Ｗ ｗ vv 𝐰 𝗐 ᴡ ѡ ա 𝔴 𝕨 𝓌 𝙬 ɯ 𝔀 𝚠 𝑤 𝘄 w 𝒘 𝘸 𝖜 ԝ
                {'y', 'γ'},     //ý Y y ʏ Υ γ у Ү Ｙ ｙ𝙮 у 𝐲 𝝲 𝑦 ᶌ 𝞬 𑣜 𝕪 ʏ 𝖞 𝚢 ｙ ꭚ 𝒚 𝓎 ɣ 𝗒 ყ 𝘆 ү 𝛾 γ 𝛄 𝔂 𝜸 y 𝔶 ℽ 𝘺 ỿ
                {'z', 'ž'}      //Z z Ζ Ꮓ Ｚ ｚ𝓏 𝙯 ᴢ 𝐳 𝗓 𑣄 𝑧 𝘇 𝕫 𝖟 𝔃 𝚣 𝔷 z 𝒛 𝘻
            };

        }
    }
}
