using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ConsoleApp1
{
    class Program
    {
        static int InsertedId;

        static void Main(string[] args)
        {
            using (var db = new ContosoUniversityEntities())
            {
                //把db所有的過程寫在console上
                db.Database.Log = Console.WriteLine;

                //是否使用延遲載入
                //db.Configuration.LazyLoadingEnabled = false;
                //是否使用代理物件
                //db.Configuration.ProxyCreationEnabled = false;

                //注意ORM的N+1地雷

                //var dept = db.Department.Find(1);

                //導覽屬性，一併把相關的Course查出來
                var dept = db.Department.Include(x => x.Course);

                foreach (var item in dept)
                {
                    Console.WriteLine(item.Name);
                    Console.WriteLine("===========================");
                    foreach (var item1 in item.Course)
                    {
                        Console.WriteLine(item1.Title);
                    }
                }

                //QueryCourse(db);

                //InsertDepartment(db);

                //UpdateDepartment(db);

                //RemoveDepartment(db);
            }
        }

        private static void InsertDepartment(ContosoUniversityEntities db)
        {
            var dept = new Department()
            {
                Name = "Will",
                Budget = 100,
                StartDate = DateTime.Now
            };

            db.Department.Add(dept);
            db.SaveChanges();

            //上述這種寫法才可以拿到自動編號的號碼
            InsertedId = dept.DepartmentID;
        }

        private static void UpdateDepartment(ContosoUniversityEntities db)
        {
            var dept = db.Department.Find(InsertedId);
            dept.Name = "John";
            db.SaveChanges();
        }

        private static void RemoveDepartment(ContosoUniversityEntities db)
        {
            db.Department.Remove(db.Department.Find(InsertedId));
            db.SaveChanges();
        }

        private static void QueryCourse(ContosoUniversityEntities db)
        {
            var data = from p in db.Course select p;

            foreach (var item in data)
            {
                Console.WriteLine(item.CourseID);
                Console.WriteLine(item.Title);
                Console.WriteLine();
            }
        }
    }
}
