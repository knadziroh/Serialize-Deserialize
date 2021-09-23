using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialize_Deserialize
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player() 
            { 
                Name = "Abe", 
                Level = 13, 
                Score = 7483
            };
            string filePath = "data.save";
            DataSerializer dataSerializer = new DataSerializer();
            Player p = null;

            dataSerializer.BinarySerialize(player, filePath);

            p = dataSerializer.BinaryDeserialize(filePath) as Player;

            Console.WriteLine("Name : " + p.Name);
            Console.WriteLine("Level : " + p.Level);
            Console.WriteLine("Score : " + p.Score);

            Console.ReadLine();
        }
    }

    [Serializable]
    public class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }

    }

    class DataSerializer
    {
        public void BinarySerialize(object data, string filePath)
        {
            FileStream fileStream;
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(filePath)) File.Delete(filePath);
            fileStream = File.Create(filePath);
            bf.Serialize(fileStream, data);
            fileStream.Close();
        }

        public object BinaryDeserialize(string filePath)
        {
            object obj = null;
            FileStream fileStream;
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(filePath))
            {
                fileStream = File.OpenRead(filePath);
                obj = bf.Deserialize(fileStream);
                fileStream.Close();
            }

            return obj;
        }
    }
}

