using System;


namespace Console3D
{
    class Program
    {
        ///////// ОПРЕДЕЛЕНИЯ СТРУКТУР  //////////////////
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
            public static vec2 operator -( vec2 A, float B) => new vec2(A.x-B,A.y-B);
            public static vec2 operator *( vec2 A, vec2 B) => new vec2(A.x*B.x,A.y*B.y);
            public static vec2 operator *( vec2 A, float B) => new vec2(A.x*B,A.y*B);
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
            public static vec3 operator *( vec3 A, float B) => new vec3(A.x*B,A.y*B,A.z*B);
            public static vec3 operator /( vec3 A, vec3 B) => new vec3(A.x/B.x,A.y/B.y,A.z/B.z);
            public static vec3 operator /( vec3 A, float B) => new vec3(A.x/B,A.y/B,A.z/B);
            public static vec3 operator /( float A, vec3 B) => new vec3(A/B.x,A/B.y,A/B.z);
            public static vec3 operator -(vec3 A) => new vec3(-A.x,-A.y,-A.z);
        }

        ////////// ФУНКЦИИ ///////////////
        float clamp(float value, float min, float max) { return MathF.Max(MathF.Min(value, max), min); }
        float sign(float a)
        {
            if (a==0) {return 0;}
            else if (a>0) {return 1;}
                else {return -1;} 
        }
        float step(float edge, float x) { if (x > edge) {return 1;} else {return 0;} }
        float length(vec2 v) { return MathF.Sqrt(v.x * v.x + v.y * v.y); }
        float length(vec3 v) { return MathF.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z); }
       vec3 norm(vec3 v) 
        {
            vec3 ret = new vec3();
            ret = v;
            return ret / length(v);
        }
        float dot(vec3 a, vec3 b) { return a.x * b.x + a.y * b.y + a.z * b.z; }
        vec3 abs(vec3 v) { return new vec3(MathF.Abs(v.x), MathF.Abs(v.y), MathF.Abs(v.z)); }
        vec3 sign(vec3 v) { return new vec3(MathF.Sign(v.x), MathF.Sign(v.y), MathF.Sign(v.z)); }
        vec3 step(vec3 edge, vec3 v) { return new vec3(step(edge.x, v.x), step(edge.y, v.y), step(edge.z, v.z)); }
        vec3 reflect(vec3 rd, vec3 n) { return rd - n * (2 * dot(n, rd)); }
        vec3 rotateX(vec3 a, float angle)
        {
            vec3 b = new vec3();
            b = a;
            b.z = a.z * MathF.Cos(angle) - a.y * MathF.Sin(angle);
            b.y = a.z * MathF.Sin(angle) + a.y * MathF.Cos(angle);
            return b;
        }
        vec3 rotateY(vec3 a, float angle)
        {
            vec3 b = new vec3(); 
            b = a;
            b.x = a.x * MathF.Cos(angle) - a.z * MathF.Sin(angle);
            b.z = a.x * MathF.Sin(angle) + a.z * MathF.Cos(angle);
            return b;
        }
        vec3 rotateZ(vec3 a, float angle)
        {
            vec3 b = new vec3();
            b = a;
            b.x = a.x * MathF.Cos(angle) - a.y * MathF.Sin(angle);
            b.y = a.x * MathF.Sin(angle) + a.y * MathF.Cos(angle);
            return b;
        }
        vec2 sphere(vec3 ro, vec3 rd, float r) 
        {
            float b = dot(ro, rd);
            float c = dot(ro, ro) - r * r;
            float h = b * b - c;
            if (h < 0.0) return new vec2((float)-1.0);
            h = MathF.Sqrt(h);
            return new vec2 (-b - h, -b + h);
        }
        vec2 box(vec3 ro, vec3 rd, vec3 boxSize, vec3 outNormal) 
        {
            vec3 m = new vec3((float)1.0) / rd;
            vec3 n = m * ro;
            vec3 k = abs(m) * boxSize;
            vec3 t1 = -n - k;
            vec3 t2 = -n + k;
            float tN = MathF.Max(MathF.Max(t1.x, t1.y), t1.z);
            float tF = MathF.Min(MathF.Min(t2.x, t2.y), t2.z);
            if (tN > tF || tF < 0.0) return new vec2((float)-1.0);
            vec3 yzx = new vec3(t1.y, t1.z, t1.x);
            vec3 zxy = new vec3(t1.z, t1.x, t1.y);
            outNormal = -sign(rd) * step(yzx, t1) * step(zxy, t1);
            return new vec2(tN, tF);
        }
        float plane(vec3 ro, vec3 rd, vec3 p, float w) {
            return -(dot(ro, p) + w) / dot(rd, p);
        }

        ///////// ПРОГРАММА!!!!! ////////
        static void Main(string[] args)
        {
            
            int width = Console.WindowWidth;
	        int height = Console.WindowHeight;

//            Console.SetWindowSize(width,height);
            float aspect = (float)width / height;
            float pixelAspect = 11.0f / 24.0f;
            string gradient = " .:!/r(l1Z4H9W8$@";
            int gradientSize = gradient.Length - 2;
            byte [] screen = new byte[width*height];

        	for (int t = 0; t < 10000; t++) 
            {
        		vec3 light = new vec3((float)-0.5, (float)0.5, (float)-1.0);
                light = norm(light);
        		vec3 spherePos = new vec3((float)0, (float)3, (float)0);

                for(int i=0; i<height; i++)
                    for (int j=0; j<width; j++)
                    {
        				vec2 uv = new vec2((float)width, (float)height);
                        uv = new vec2((float)i, (float)j) / uv * 2.0f - 1.0f;
        				uv.x *= aspect * pixelAspect;
        				vec3 ro = new vec3(-6, 0, 0);
				        vec3 rd = norm(new vec3(2, uv));
				        ro = rotateY(ro, (float)0.25);
				        rd = rotateY(rd, (float)0.25);
				        ro = rotateZ(ro, (float)(t * 0.01));
				        rd = rotateZ(rd, (float)(t * 0.01));
				        float diff = 1;
                        /// тут остановился
                        screen[i+j*height]=(byte)'@';
                    }
                var stdout = Console.OpenStandardOutput(width * height);
                stdout.Write(screen,0,screen.Length);
            }
        }
    }
}
