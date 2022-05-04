using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DoodleJump
{
    class Camera
    {
        public Matrix transform;
        Viewport view;
        Vector2 centre;
        public Vector2 cameraPos;

        public Camera(Viewport newView)
        {
            cameraPos = new Vector2(0, 0);
            view = newView;
        }

        public void setPos(Vector2 newCameraPos)
        {
            cameraPos = newCameraPos;
            centre = newCameraPos;
            transform = Matrix.CreateScale(new Vector3(1,1,0))*
                Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y,0));

        }

    }
}
