using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3d
{
    public class Point
    {
        public double x, y, z;

        public static Point operator +(Point p0, Point p1)
        {
            return new Point { x = p0.x + p1.x, y = p0.y + p1.y, z = p0.z + p1.z };
        }

        public static Point operator -(Point p0, Point p1)
        {
            return new Point { x = p0.x - p1.x, y = p0.y - p1.y, z = p0.z - p1.z };
        }

        public static Point operator /(Point p, double f)
        {
            return new Point { x = p.x / f, y = p.y / f, z = p.z / f };
        }

        public static Point operator *(Point p, double f)
        {
            return new Point { x = p.x * f, y = p.y * f, z = p.z * f };
        }

        public void normalize()
        {
            double l = lenght();
            x /= l;
            y /= l;
            z /= l;
        }
        public double lenght()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }
        public double dot(Point src)
        {
            return x * src.x + y * src.y + z * src.z;
        }
        public static Point cross(Point src0, Point src1)
        {
            return new Point
            {
                x = src0.y * src1.z - src0.z * src1.y,
                y = src0.z * src1.x - src0.x * src1.z,
                z = src0.x * src1.y - src0.y * src1.x
            };
        }
    }

    public class Matrix3
    {
        public double[,] v = new double[3, 3];

        public Matrix3()
        {
            v[0, 0] = v[1, 1] = v[2, 2] = 1.0f;
            v[0, 1] = v[1, 0] = v[0, 2] =
            v[2, 0] = v[2, 1] = v[1, 2] = 0.0f;
        }

        public static Point operator *(Matrix3 m, Point p)
        {
            return new Point
            {
                x = m.v[0, 0] * p.x + m.v[0, 1] * p.y + m.v[0, 2] * p.z,
                y = m.v[1, 0] * p.x + m.v[1, 1] * p.y + m.v[1, 2] * p.z,
                z = m.v[2, 0] * p.x + m.v[2, 1] * p.y + m.v[2, 2] * p.z
            };
        }

        public static Matrix3 operator *(Matrix3 m0, Matrix3 m1)
        {
            Matrix3 m = new Matrix3();

            m.v[0, 0] = m0.v[0, 0] * m1.v[0, 0] + m0.v[0, 1] * m1.v[1, 0] + m0.v[0, 2] * m1.v[2, 0];
            m.v[0, 1] = m0.v[0, 0] * m1.v[0, 1] + m0.v[0, 1] * m1.v[1, 1] + m0.v[0, 2] * m1.v[2, 1];
            m.v[0, 2] = m0.v[0, 0] * m1.v[0, 2] + m0.v[0, 1] * m1.v[1, 2] + m0.v[0, 2] * m1.v[2, 2];
            m.v[1, 0] = m0.v[1, 0] * m1.v[0, 0] + m0.v[1, 1] * m1.v[1, 0] + m0.v[1, 2] * m1.v[2, 0];
            m.v[1, 1] = m0.v[1, 0] * m1.v[0, 1] + m0.v[1, 1] * m1.v[1, 1] + m0.v[1, 2] * m1.v[2, 1];
            m.v[1, 2] = m0.v[1, 0] * m1.v[0, 2] + m0.v[1, 1] * m1.v[1, 2] + m0.v[1, 2] * m1.v[2, 2];
            m.v[2, 0] = m0.v[2, 0] * m1.v[0, 0] + m0.v[2, 1] * m1.v[1, 0] + m0.v[2, 2] * m1.v[2, 0];
            m.v[2, 1] = m0.v[2, 0] * m1.v[0, 1] + m0.v[2, 1] * m1.v[1, 1] + m0.v[2, 2] * m1.v[2, 1];
            m.v[2, 2] = m0.v[2, 0] * m1.v[0, 2] + m0.v[2, 1] * m1.v[1, 2] + m0.v[2, 2] * m1.v[2, 2];

            return m;
        }

        public Matrix3 transpose()
        {
            Matrix3 o = new Matrix3();

            o.v[0, 1] = v[1, 0]; o.v[1, 0] = v[0, 1];
            o.v[0, 2] = v[2, 0]; o.v[2, 0] = v[0, 2];
            o.v[1, 2] = v[2, 1]; o.v[2, 1] = v[1, 2];
            o.v[0, 0] = v[0, 0];
            o.v[1, 1] = v[1, 1];
            o.v[2, 2] = v[2, 2];

            return o;
        }

        public Matrix3 generate_eigen_vectors()
        {
            Matrix3 o = new Matrix3();
            Point[] e = new Point[3] { new Point(), new Point(), new Point() };
            Random cRandom = new System.Random();

            for (int i = 0; i < 2; i++)
            {
                // 適当に初期化
                e[i].x = (double)cRandom.Next();
                e[i].y = (double)cRandom.Next();
                e[i].z = (double)cRandom.Next();
                // ベクトルを正規化しつつ行列にかけ続け、変化がなくなるベクトルが固有ベクトル
                // それを全てのベクトルが直交化するように、射影ながら続ける
                for (int j = 0; j < 100; j++)
                {

                    e[i] = this * e[i];
                    e[i].normalize();

                    // 既に求まった固有ベクトルの向きの成分を引いて計算を行う
                    for (int k = 0; k < i; k++)
                    {
                        e[i] = e[i] - e[k] * e[i].dot(e[k]);
                    }
                    e[i].normalize();
                }
            }
            // 3つめは、他の2つと直交することから求められる
            e[2] = Point.cross(e[0], e[1]);

            o.v[0, 0] = e[0].x; o.v[1, 0] = e[0].y; o.v[2, 0] = e[0].z;
            o.v[0, 1] = e[1].x; o.v[1, 1] = e[1].y; o.v[2, 1] = e[1].z;
            o.v[0, 2] = e[2].x; o.v[1, 2] = e[2].y; o.v[2, 2] = e[2].z;

            return o;
        }

        public static Matrix3 identity()
        {
            Matrix3 m = new Matrix3();

            m.v[0, 0] = m.v[1, 1] = m.v[2, 2] = 1.0f;
            m.v[0, 1] = m.v[1, 0] = m.v[0, 2] =
            m.v[2, 0] = m.v[2, 1] = m.v[1, 2] = 0.0f;

            return m;
        }

        public static Matrix3 create(Point[] src)
        {
            Matrix3 m = new Matrix3();

            m.v[0, 0] = src[0].x;
            m.v[0, 1] = src[1].x;
            m.v[0, 2] = src[2].x;
            m.v[1, 0] = src[0].y;
            m.v[1, 1] = src[1].y;
            m.v[1, 2] = src[2].y;
            m.v[2, 0] = src[0].z;
            m.v[2, 1] = src[1].z;
            m.v[2, 2] = src[2].z;

            return m;
        }
    }

    public class CoordinateTransform
    {

        Matrix3 R;
        Point T;

        public CoordinateTransform()
        {
            R = Matrix3.identity();
            T = new Point { x = 0, y = 0, z = 0 };
        }

        public void initialize(Point[] o, Point[] i)
        {
            // 重心を求める
            Point ave_i = (i[0] + i[1] + i[2]) / 3;
            Point ave_o = (o[0] + o[1] + o[2]) / 3;

            // 相対座標にする
            Point[] rel_i = new Point[3] { i[0] - ave_i, i[1] - ave_i, i[2] - ave_i };
            Point[] rel_o = new Point[3] { o[0] - ave_o, o[1] - ave_o, o[2] - ave_o };

            Matrix3 mi = Matrix3.create(rel_i);
            Matrix3 mo = Matrix3.create(rel_o);

            Matrix3 A = mi * mo.transpose();

            Matrix3 AAT = A * A.transpose();
            Matrix3 ATA = A.transpose() * A;

            Matrix3 U = AAT.generate_eigen_vectors();
            Matrix3 V = ATA.generate_eigen_vectors();
            R = V * U.transpose();

            // Rを求める
            // RからTを求める
            T = ave_o - R * ave_i;
        }

        public Point convert(Point src)
        {
            return R * src + T;
        }
    }
}
