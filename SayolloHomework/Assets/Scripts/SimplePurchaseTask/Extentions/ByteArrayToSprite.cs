using UnityEngine;

namespace AndriiYefimov.SayolloHW2.Extentions
{
    public static class ByteArrayToSprite
    {
        public static Sprite ToSprite(this byte[] data)
        {
            var tex = new Texture2D(1,1);
            tex.LoadImage(data);
            return Sprite.Create(tex, 
                new Rect(0, 0, tex.width, tex.height), 
                new Vector2(tex.width/2, tex.height/2));
        }
    }
}