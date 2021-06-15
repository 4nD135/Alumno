using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Comentarios
{
    class Comentario
    {
        public string id { get; set; }
        public string autor { get; set; }
        public DateTime fecha_de_publicacion { get; set; }
        public string comentario { get; set; }
        public string direccion_ip { get; set; }
        public bool es_inapropiado { get; set; }
        public int likes { get; set; }

        public override string ToString()
        {
            return String.Format($"{autor} - Comentario: {comentario} - Es inapropiado: {es_inapropiado} - Ip: {direccion_ip} - Fecha: {fecha_de_publicacion} - Likes: {likes} | id:{id}");
        }

    }

    class ComentarioDB
    {
        public static void SaveToFile( List<Comentario> comentarios, string path)
        {
            StreamWriter textOut = null;

            try
            {
                textOut = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));
                foreach(var comentario in comentarios)
                {
                    textOut.Write(comentario.autor + "|");
                    textOut.Write(comentario.comentario + "|");
                    textOut.Write(comentario.direccion_ip + "|");
                    textOut.Write(comentario.es_inapropiado + "|");
                    textOut.Write(comentario.fecha_de_publicacion + "|");
                    textOut.Write(comentario.likes + "|");
                    textOut.WriteLine(comentario.id);
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (textOut != null)
                    textOut.Close();
            }
        }
        
        public static List<Comentario> ReadFromFile(string path)
        {
            List<Comentario> comentarios = new List<Comentario>();

            StreamReader textIn = new StreamReader(new FileStream(path,FileMode.Open,FileAccess.Read));
            try
            {
                while (textIn.Peek() != -1)
                {
                    string row = textIn.ReadLine();
                    string[] colums = row.Split("|");
                    Comentario c = new Comentario();
                    c.autor = colums[0];
                    c.comentario = colums[1];
                    c.direccion_ip = colums[2];
                    c.es_inapropiado = bool.Parse(colums[3]);
                    c.fecha_de_publicacion = DateTime.Parse(colums[4]);
                    c.likes = int.Parse(colums[5]);
                    c.id = colums[6];
                    comentarios.Add(c);
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                textIn.Close();
            }
            return comentarios;
        }
        
        public static void GetLikes(string path)
        {
            List<Comentario> comentarios;
            comentarios = ReadFromFile(path);
            var filtro_comentarios = from c in comentarios
                                     orderby c.likes descending
                                     select c;
            foreach (var c in filtro_comentarios)
                if (c.es_inapropiado == false)
                    Console.WriteLine(c);
        }
        
        public static void GetDate(string path)
        {
            List<Comentario> comentarios;
            comentarios = ReadFromFile(path);
            var filtro_comentarios = from c in comentarios
                                     orderby c.fecha_de_publicacion descending
                                     select c;
            foreach (var c in filtro_comentarios)
                if (c.es_inapropiado == false)
                    Console.WriteLine(c);
        }

    }

    class Program
    {
        public static void BorraComentario(List<Comentario> comentarios)
        {
            Console.Clear();
            Console.WriteLine("Porfavor ingrese el id del comentario que desea borrar");
            string r = Console.ReadLine();

            comentarios.RemoveAll(a => a.id.Contains(r));
            foreach (var c in comentarios.Where(c => c.es_inapropiado == false))
            {
                Console.WriteLine(c);
            }

            Console.ReadKey();
        }
        
        static void Main(string[] args)
        {
            /*
            List<Comentario> comentarios = new List<Comentario>();

            comentarios.Add(new Comentario() 
            { 
                autor = "Donaldo Octavio", 
                comentario = "Nice clip bro", 
                direccion_ip = "129.213.311", 
                es_inapropiado = true, 
                fecha_de_publicacion = new DateTime(2020, 12, 11, 22, 5, 0), 
                likes = 20, 
                id = "AA073"
            });
            
            comentarios.Add(new Comentario()
            {
                autor = "Iris Slate",
                comentario = "Great video!",
                direccion_ip = "129.344.421",
                es_inapropiado = false,
                fecha_de_publicacion = new DateTime(2020, 10, 13, 10, 25, 32),
                likes = 25,
                id = "AA025"
            });

            comentarios.Add(new Comentario()
            {
                autor = "Nick Hudson",
                comentario = "First comment",
                direccion_ip = "129.142.112",
                es_inapropiado = false,
                fecha_de_publicacion = new DateTime(2020, 10, 12, 12, 21 ,0),
                likes = 1,
                id = "AA012"
            });

            comentarios.Add(new Comentario()
            {
                autor = "Frank Acosta",
                comentario = "Too short",
                direccion_ip = "129.117.120",
                es_inapropiado = false,
                fecha_de_publicacion = new DateTime(2020, 11, 25, 18, 45, 13),
                likes = 4,
                id = "AA015"
            });

            ComentarioDB.SaveToFile(comentarios,@"C:\Users\ret-z\Downloads\prueba\comentarios.txt");
            */
            
            List<Comentario> comentarios = ComentarioDB.ReadFromFile(@"C:\Users\ret-z\Downloads\prueba\comentarios.txt");
            foreach (var c in from c in comentarios
                              where c.es_inapropiado == false
                              select c)
            {
                Console.WriteLine(c);
            }

            Console.WriteLine();
            Console.WriteLine("| Ordenados por likes |");
            ComentarioDB.GetLikes(@"C:\Users\ret-z\Downloads\prueba\comentarios.txt");

            Console.WriteLine();
            Console.WriteLine("| Ordenados por fecha |");
            ComentarioDB.GetDate(@"C:\Users\ret-z\Downloads\prueba\comentarios.txt");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("¿Desea borrar algún comentario? [S/N]");
            string respuesta = Console.ReadLine();
            if (respuesta == "S" | respuesta == "s")
            {
                BorraComentario(comentarios);
            }
            else if (respuesta == "N" | respuesta == "n")
            {
                Console.Clear();
                Console.WriteLine("Gracias por participar en esta prueba, que tenga un buen día");
            }
            else if (respuesta == "" | respuesta != "")
            {
                Console.Clear();
                Console.WriteLine("No ingresó un formato válido");
            }
            Console.ReadKey();
        }
    }
}
