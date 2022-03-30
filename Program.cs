using System;


namespace Console3D
{
    class Program
    {
        struct vec2
        {
            public float x;
            public float y;

            public vec2(float value)
            {
                this.x=value;
                this.y=value;
            }
            public vec2(float _x, float _y)
            {
                this.x=_x;
                this.y=_y;
            }

            public static vec2 operator +( vec2 A, vec2 B) => new vec2(A.x+B.x,A.y+B.y);
            public static vec2 operator -( vec2 A, vec2 B) => new vec2(A.x-B.x,A.y-B.y);
            public static vec2 operator *( vec2 A, vec2 B) => new vec2(A.x*B.x,A.y*B.y);
            public static vec2 operator /( vec2 A, vec2 B) => new vec2(A.x/B.x,A.y/B.y);
        };

        struct vec3
        {
            public float x;
            public float y;
            public float z;

            public vec3(float value)
            {
                this.x=value;
                this.y=value;
                this.z=value;
            }
            public vec3(float _x, vec2 _v)
            {
                this.x=_x;
                this.y=_v.x;
                this.z=_v.y;
            }
            public vec3(float _x, float _y, float _z)
            {
                this.x=_x;
                this.y=_y;
                this.z=_z;
            }
            public static vec3 operator +( vec3 A, vec3 B) => new vec3(A.x+B.x,A.y+B.y,A.z+B.z);
            public static vec3 operator -( vec3 A, vec3 B) => new vec3(A.x-B.x,A.y-B.y,A.z-B.z);
            public static vec3 operator *( vec3 A, vec3 B) => new vec3(A.x*B.x,A.y*B.y,A.z*B.z);
            public static vec3 operator /( vec3 A, vec3 B) => new vec3(A.x/B.x,A.y/B.y,A.z/B.z);
            public static vec3 operator -(vec3 A) => new vec3(-A.x,-A.y,-A.z);
        }

        float clamp(float value, float min, float max) { return MathF.Max(MathF.Min(value, max), min); }
        double sign(double a)
        {
            if (a==0) {return 0;}
            else if (a>0) {return 1;}
                else {return -1;} 
        }
        double step(double edge, double x) { if (x > edge) {return 1;} else {return 0;} }


        ///////// ПРОГРАММА!!!!! ////////
        static void Main(string[] args)
        {
            
            int width = Console.WindowWidth;
	        int height = Console.WindowHeight;

//            Console.SetWindowSize(width,height);
            float aspect = (float)width / height;
//            float pixelAspect = 11.0f / 24.0f;
            string gradient = " .:!/r(l1Z4H9W8$@";
            int gradientSize = gradient.Length - 2;
            byte [] screen = new byte[width*height];


            for(int i=0; i<height; i++)
                for (int j=0; j<width; j++)
                {
                    screen[i+j*height]=(byte)'@';
                }
            var stdout = Console.OpenStandardOutput(width * height);
            stdout.Write(screen,0,screen.Length);
        }
    }
}
