using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using System.Text.Json;

namespace ConsoleApp16
{

    class Id_Name
    {
        public Id_Name()
        {

        }
        public Id_Name(string? name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
        }

        private string? Name1 { get; set; }
        public string? Id { get; set; }

        public string? Name
        {
            get
            {
                return Name1;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Invalid input name..!");
                }

                Name1 = value;
            }
        }



    }


    class Academy : Id_Name
    {
        public Academy(string? Name) : base(Name)
        {
            this.Groups = new List<Group>();
            this.Teachers = new List<Teacher>();
            this.Students = new List<Student>();
        }
        public List<Group>? Groups { get; set; }
        public List<Teacher>? Teachers { get; set; }
        public List<Student>? Students { get; set; }

    }

    class Group : Id_Name
    {
        public Group(string? Name, string? TeacherId) : base(Name)
        {
            this.TeacherId = TeacherId;
        }
        public string? TeacherId { get; set; }
    }

    class Teacher : Id_Name
    {
        public Teacher(string? Name, string? Surname, double Salary) : base(Name)
        {

            this.Surname = Surname;
            this.Salary = Salary;
        }
        private string? Surname1 { get; set; }

        public double Salary1 { get; set; }

        public string? GroupId { get; set; }

        public string? Surname
        {
            get
            {
                return Surname1;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Contains(" "))
                {
                    throw new ArgumentException("Invalid input surname..!");
                }
                Surname1 = value;
            }
        }
        public double Salary
        {
            get
            {
                return Salary1;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Invalid input salary");
                }
                Salary1 = value;
            }
        }


    }

    class Student : Id_Name
    {
        public Student(string? Name, string? Surname) : base(Name)
        {
            this.Surname = Surname;
        }
        private string? Surname1 { get; set; }
        public string? Surname
        {
            get
            {
                return Surname1;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Contains(" "))
                {
                    throw new ArgumentException("Invalid input surname..!");
                }
                Surname1 = value;
            }
        }

        public double Score { get; set; } = 0;

        public string? GroupId { get; set; }
    }

    static class New_GTS
    {
        public static void NewGroup(Academy? academy)
        {
            Console.Write("Qrupun adini daxil edin: ");
            string? Name;
            Name = Console.ReadLine();
            foreach (var item in academy.Groups)
            {
                if (item.Name == Name)
                {
                    Console.WriteLine("Bu adda basqa qrup movcuddur..!");
                    return;
                }
            }
            if (academy.Teachers?.Count == 0 || academy.Teachers == null)
            {
                Console.WriteLine("Hal-hazirda academiyada muellim yoxdur..!");
                return;
            }
            Console.WriteLine("Hansi muellimi elave etmek isteyirsiniz?");
            int no = 0;
            foreach (var item in academy.Teachers)
            {
                Console.WriteLine("---------------------------------------------------------------->");
                Console.WriteLine($"{++no}.Adi: {item.Name} , Soyadi: {item.Surname} , Id-i: {item.Id}");
                Console.WriteLine("---------------------------------------------------------------->");
            }
            Console.WriteLine("Secdiyiniz muellimin Id-i bura yazin");
            Group group;
            string? choice = Console.ReadLine();
            foreach (var item in academy.Teachers)
            {
                if (item.Id == choice)
                {

                    group = new Group(Name, item.Id);
                    item.GroupId = group.Id;
                    academy.Groups?.Add(group);
                    Console.WriteLine($"Yeni qrup \"{group.Name}\" ugurla elave edildi.!");
                    return;

                }
            }
            Console.WriteLine("Bu Id-e uygun muellim tapilmadi..!");


        }

        public static void DeleteGroup(Academy? academy)
        {
            if (academy?.Groups?.Count == 0 || academy?.Groups == null)
            {
                Console.WriteLine("Hal-hazirda academiyada qrup yoxdur..!");
                return;
            }
            Console.WriteLine("Hansi qrupu silmek isteyirsiniz.?");
            int no = 0;
            foreach (var item in academy.Groups)
            {
                Console.WriteLine("---------------------------------------------------------------->");
                Console.WriteLine($"{++no}.Adi: {item.Name} , Qrupun Id-i: {item.Id}");
                Console.WriteLine("---------------------------------------------------------------->");
            }
            Console.WriteLine("Silmek istediyiniz qrupun adini daxil edin.");
            string? choice = Console.ReadLine();
            foreach (var item in academy.Groups)
            {
                if (item.Name == choice)
                {
                    Console.WriteLine($"Qrup {item.Name} siyahidan ugurla silindi.!");
                    academy.Groups.Remove(item);
                    return;

                }
            }
            Console.WriteLine("Bu adda qrup movcud deyil.!");
        }


        public static void NewTeacher(Academy? academy)
        {
            Console.Write("Mellimin adini daxil edin: ");
            string? Name = Console.ReadLine();
            Console.Write("Mellimin soyadini daxil edin: ");
            string? Surname = Console.ReadLine();
            Console.Write("Mellimin ayliq maasini daxil edin: ");
            double Salary = Convert.ToDouble(Console.ReadLine());
            Teacher teacher = new Teacher(Name, Surname, Salary);
            academy?.Teachers?.Add(teacher);
            Console.WriteLine($"Yeni muellim \"{teacher.Name}\" ugurla elave edildi.!");
        }
        public static void DeleteTeacher(Academy? academy)
        {
            if (academy.Teachers.Count == 0 || academy.Teachers == null)
            {
                Console.WriteLine("Hal-hazirda academiyada muellim yoxdur..!");
                return;
            }
            Console.WriteLine("Hansi muellimi silmek isteyirsiniz.?");
            int no = 0;
            foreach (var item in academy.Teachers)
            {
                Console.WriteLine("---------------------------------------------------------------->");
                Console.WriteLine($"{++no}.Adi: {item.Name} , Qrupun Id-i: {item.Surname} , Id-i: {item.Id}");
                Console.WriteLine("---------------------------------------------------------------->");
            }
            Console.WriteLine("Silmek istediyiniz muellimin Id-i daxil edin.");
            string? choice = Console.ReadLine();

            foreach (var item in academy.Teachers)
            {
                if (item.Id == choice)
                {

                    Console.WriteLine($"Muellim {item.Name} siyahidan ugurla silindi.!");
                    academy.Teachers.Remove(item);
                    return;

                }
            }
            Console.WriteLine("Bu Id-e uygun muellim tapilmadi..!");
        }

        public static void NewStudent(Academy? academy)
        {
            Console.Write("Telebenin adini daxil edin: ");
            string? Name = Console.ReadLine();
            Console.Write("Telebenin soyadini daxil edin: ");
            string? Surname = Console.ReadLine();
            Student student = new Student(Name, Surname);
            academy.Students?.Add(student);
            Console.WriteLine($"Yeni telebe \"{student.Name}\" ugurla elave edildi.!");
        }
        public static void DeleteStudent(Academy? academy)
        {
            if (academy?.Students?.Count == 0 || academy?.Students == null)
            {
                Console.WriteLine("Hal-hazirda academiyada telebe yoxdur..!");
                return;
            }
            Console.WriteLine("Hansi telebeni silmek isteyirsiniz.?");
            int no = 0;
            foreach (var item in academy.Students)
            {
                Console.WriteLine("---------------------------------------------------------------->");
                Console.WriteLine($"{++no}.Adi: {item.Name} , Qrupun Id-i: {item.Surname} , Id-i: {item.Id}");
                Console.WriteLine("---------------------------------------------------------------->");
            }
            Console.WriteLine("Silmek istediyiniz telebenin Id-i daxil edin.");
            string? choice = Console.ReadLine();
            foreach (var item in academy.Students)
            {

                if (item.Id == choice)
                {
                    Console.WriteLine($"Telebe {item.Name} siyahidan ugurla silindi.!");
                    academy.Students.Remove(item);
                    return;
                }

            }
            Console.WriteLine("Bu Id-e uygun telebe tapilmadi..!");
        }
        public static void AddStudentGroup(Academy? academy)
        {
            if (academy?.Groups?.Count == 0 || academy?.Groups == null)
            {
                Console.WriteLine("Hal-hazirda academiyada qrup yoxdur..!");
                return;
            }
            Console.WriteLine("Hansi qrupa telebe elave etmek isteyirsiniz.?");
            int no = 0;
            foreach (var item in academy.Groups)
            {
                Console.WriteLine("---------------------------------------------------------------->");
                Console.WriteLine($"{++no}.Adi: {item.Name} , Qrupun Id-i: {item.Id}");
                Console.WriteLine("---------------------------------------------------------------->");
            }
            Console.WriteLine("qrupun adini daxil edin.");
            string? choice = Console.ReadLine();
            foreach (var item1 in academy.Groups)
            {
                if (item1.Name == choice)
                {
                    if (academy?.Students.Count == 0 || academy?.Students == null)
                    {
                        Console.WriteLine("Hal-hazirda academiyada telebe yoxdur..!");
                        return;
                    }
                    Console.WriteLine("Hansi telebeni elave etmek isteyirsiniz.?");
                    int no1 = 0;
                    foreach (var item in academy.Students)
                    {
                        Console.WriteLine("---------------------------------------------------------------->");
                        Console.WriteLine($"{++no1}.Adi: {item.Name} , Qrupun Id-i: {item.Surname} , Id-i: {item.Id}");
                        Console.WriteLine("---------------------------------------------------------------->");
                    }
                    Console.WriteLine("telebenin Id-i daxil edin.");
                    string? choice1 = Console.ReadLine();
                    foreach (var items in academy.Students)
                    {
                        if (items.Id == choice1)
                        {
                            items.GroupId = item1.Id;
                            Console.WriteLine("Qrupa telebe ugurla elave edildi.!");
                            return;

                        }
                    }
                    Console.WriteLine("Bu Id-e uygun telebe tapilmadi..!");
                    return;

                }
            }


        }
        public static void DeleteStudentGroup(Academy? academy)
        {
            if (academy?.Groups?.Count == 0 || academy?.Groups == null)
            {
                Console.WriteLine("Hal-hazirda academiyada qrup yoxdur..!");
                return;
            }

            Console.WriteLine("Hansi qrupdan telebe silmek isteyirsiniz.?");
            int no = 0;
            foreach (var item in academy.Groups)
            {
                Console.WriteLine("---------------------------------------------------------------->");
                Console.WriteLine($"{++no}.Adi: {item.Name} , Qrupun Id-i: {item.Id}");
                Console.WriteLine("---------------------------------------------------------------->");
            }
            Console.WriteLine("qrupun adini daxil edin.");
            string? choice = Console.ReadLine();

            int stdent = 0;
            if (academy?.Students?.Count == 0 || academy?.Students == null)
            {
                Console.WriteLine("Hal-hazirda academiyada telebe yoxdur..!");
                return;
            }
            foreach (var item1 in academy.Groups)
            {
                if (item1.Name == choice)
                {
                    foreach (var item2 in academy.Students)
                    {
                        if (item2.GroupId == item1.Id)
                        {
                            stdent++;
                        }
                    }
                    if (stdent == 0)
                    {
                        Console.WriteLine("Qrupda telebe movcud deyil.!");
                        return;
                    }

                    Console.WriteLine("Hansi telebeni qrupdan silmek isteyirsiniz.?");
                    int no1 = 0;
                    foreach (var item in academy.Students)
                    {
                        Console.WriteLine("---------------------------------------------------------------->");
                        Console.WriteLine($"{++no1}.Adi: {item.Name} , Soyadi: {item.Surname} , Id-i: {item.Id}");
                        Console.WriteLine("---------------------------------------------------------------->");
                    }
                    Console.WriteLine("telebenin Id-i daxil edin.");
                    string? choice1 = Console.ReadLine();
                    foreach (var item in academy.Students)
                    {
                        if (item.Id == choice1)
                        {

                            item.GroupId = null;
                            Console.WriteLine("Telebe qrupdan ugurla silindi.!");
                            return;

                        }
                    }
                    Console.WriteLine("Bu Id-e uygun telebe tapilmadi..!");
                    return;

                }
            }


        }

        public static void ShowGroup(Academy academy)
        {
            if (academy.Groups == null || academy.Groups.Count == 0)
            {
                Console.WriteLine("Hal-hazirda academiyada qrup yoxdur..!");
                return;
            }
            foreach (var item in academy.Groups)
            {
                Console.WriteLine("---------------------------------------------------------------->");
                Console.WriteLine($"Qrupun adi: {item.Name} , Qrupun id-si: {item.Id}");
                Console.WriteLine("---------------------------------------------------------------->");
            }

        }
        public static void ShowTeacher(Academy academy)
        {
            if (academy.Teachers == null || academy.Teachers.Count == 0)
            {
                Console.WriteLine("Hal-hazirda academiyada muellim yoxdur..!");
                return;
            }
            foreach (var item in academy.Teachers)
            {
                Console.WriteLine("---------------------------------------------------------------->");
                Console.WriteLine($"Muellimin adi: {item.Name} , Muellimin soyadi: {item.Surname} , Ayliq maasi: {item.Salary}");
                Console.WriteLine("---------------------------------------------------------------->");
            }
        }
        public static void ShowStudent(Academy academy)
        {
            if (academy.Students == null || academy.Students.Count == 0)
            {
                Console.WriteLine("Hal-hazirda academiyada telebe yoxdur..!");
                return;
            }
            foreach (var item in academy.Students)
            {
                Console.WriteLine("---------------------------------------------------------------->");
                Console.WriteLine($"Telebenin adi: {item.Name} , Telebenin soyadi: {item.Surname} , Orta qiymeti: {item.Score}");
                Console.WriteLine("---------------------------------------------------------------->");
            }
        }

        public static void SearchGroup(Academy academy)
        {
            if (academy?.Groups?.Count == 0 || academy?.Groups == null)
            {
                Console.WriteLine("Hal-hazirda academiyada qrup yoxdur..!");
                return;
            }
            Console.WriteLine("Baxmaq istediyiniz qrupun adini daxil edin.");
            string? choice = Console.ReadLine();
            int no1 = 0;
            int no = 0;
            foreach (var item in academy.Groups)
            {
                if (item.Name == choice)
                {
                    Console.WriteLine("---------------------------------------------------------------->");
                    Console.WriteLine($"Qrupun adi: {item.Name} , Qrupun id-si: {item.Id}");
                    Console.WriteLine("---------------------------------------------------------------->\n");
                    Console.WriteLine("_________ Qrupun muellimi __________");
                    foreach (var ite in academy.Teachers)
                    {

                        if (ite.GroupId == item.Id)
                        {
                            Console.WriteLine("---------------------------------------------------------------->");
                            Console.WriteLine($"{++no}.Adi: {ite.Name} , Soyadi: {ite.Surname} , Ayliq maasi: {ite.Salary}");
                            Console.WriteLine("---------------------------------------------------------------->");
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("_________ Qrupun telebeleri __________");
                    foreach (var ite in academy.Students)
                    {
                        if (ite.GroupId == item.Id)
                        {
                            Console.WriteLine("---------------------------------------------------------------->");
                            Console.WriteLine($"{++no1}.Adi: {ite.Name} , Soyadi: {ite.Surname} , Orta qiymeti: {ite.Score}");
                            Console.WriteLine("---------------------------------------------------------------->");
                        }
                    }
                    Console.WriteLine();
                    return;

                }
            }
            Console.WriteLine("Bu adda qrup movcud deyil.!");
        }
        public static void SearchTeacher(Academy academy)
        {
            if (academy.Teachers?.Count == 0 || academy.Teachers == null)
            {
                Console.WriteLine("Hal-hazirda academiyada muellim yoxdur..!");
                return;
            }

            Console.WriteLine("Axtardiginiz muellimin Id-i bura yazin");
            Group group;
            string? choice = Console.ReadLine();
            foreach (var item in academy.Teachers)
            {
                if (item.Id == choice)
                {

                    Console.WriteLine("---------------------------------------------------------------->");
                    Console.WriteLine($"Muellimin adi: {item.Name} , Muellimin soyadi: {item.Surname} , Ayliq maasi: {item.Salary}");
                    Console.WriteLine("---------------------------------------------------------------->");
                    return;

                }
            }
            Console.WriteLine("Bu Id-e uygun muellim tapilmadi..!");
        }
        public static void SearchStudent(Academy academy)
        {
            if (academy.Students?.Count == 0 || academy.Students == null)
            {
                Console.WriteLine("Hal-hazirda academiyada telebe yoxdur..!");
                return;
            }

            Console.WriteLine("Axtardiginiz telebenin Id-i bura yazin");
            Group group;
            string? choice = Console.ReadLine();
            foreach (var item in academy.Students)
            {
                if (item.Id == choice)
                {

                    Console.WriteLine("---------------------------------------------------------------->");
                    Console.WriteLine($"Telebenin adi: {item.Name} , Telebenin soyadi: {item.Surname} , Orta qiymeti: {item.Score}");
                    Console.WriteLine("---------------------------------------------------------------->");
                    return;

                }
            }
            Console.WriteLine("Bu Id-e uygun telebe tapilmadi..!");
        }

        public static void UpdateGroup(Academy academy)
        {
            if (academy?.Groups?.Count == 0 || academy?.Groups == null)
            {
                Console.WriteLine("Hal-hazirda academiyada qrup yoxdur..!");
                return;
            }
            Console.WriteLine("Adini deyismek istediyiniz qrupun adini daxil edin.");
            string? choice = Console.ReadLine();
            int no1 = 0;
            int no = 0;

            foreach (var item in academy.Groups)
            {
                if (item.Name == choice)
                {
                    Console.WriteLine("---------------------------------------------------------------->");
                    Console.WriteLine($"Qrupun adi: {item.Name} , Qrupun id-si: {item.Id}");
                    Console.WriteLine("---------------------------------------------------------------->\n");
                    Console.WriteLine("Qrupun yeni adini daxil edin");
                    string? name = Console.ReadLine();
                    foreach (var namee in academy.Groups)
                    {
                        if (namee.Name == name)
                        {
                            Console.WriteLine("Bu adda basqa qrup movcuddur.!");
                            return;
                        }
                    }
                    Console.WriteLine($"{item.Name} qrupunun adi ugurla deyisdirildi.!");
                    item.Name = name;
                    Console.WriteLine($"Qrupun yeni adi: {item.Name} ");
                    return;

                }
            }
            Console.WriteLine("Bu adda qrup movcud deyil.!");
        }
        public static void UpdateTeacher(Academy academy)
        {
            if (academy.Teachers?.Count == 0 || academy.Teachers == null)
            {
                Console.WriteLine("Hal-hazirda academiyada muellim yoxdur..!");
                return;
            }

            Console.WriteLine("Axtardiginiz muellimin Id-i bura yazin");
            string? choice = Console.ReadLine();
            foreach (var item in academy.Teachers)
            {
                if (item.Id == choice)
                {

                    Console.WriteLine("---------------------------------------------------------------->");
                    Console.WriteLine($"Muellimin adi: {item.Name} , Muellimin soyadi: {item.Surname} , Ayliq maasi: {item.Salary}");
                    Console.WriteLine("---------------------------------------------------------------->");
                    Console.WriteLine("Muellimin yeni maasini daxil edin");
                    double salary = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine($"{item.Name} muellimin maasi ugurla deyisdirildi.!");
                    item.Salary = salary;
                    return;

                }
            }
            Console.WriteLine("Bu Id-e uygun muellim tapilmadi..!");
        }
        public static void UpdateStudent(Academy academy)
        {
            if (academy.Students?.Count == 0 || academy.Students == null)
            {
                Console.WriteLine("Hal-hazirda academiyada telebe yoxdur..!");
                return;
            }

            Console.WriteLine("Axtardiginiz telebenin Id-i bura yazin");
            string? choice = Console.ReadLine();
            foreach (var item in academy.Students)
            {
                if (item.Id == choice)
                {

                    Console.WriteLine("---------------------------------------------------------------->");
                    Console.WriteLine($"Telebenin adi: {item.Name} , Telebenin soyadi: {item.Surname} , Orta qiymeti: {item.Score}");
                    Console.WriteLine("---------------------------------------------------------------->");
                    Console.WriteLine("Telebenin yeni qiymetini daxil edin");
                    double score = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine($"{item.Name} telebenin qiymeti ugurla deyisdirildi.!");
                    item.Score = score;
                    return;

                }
            }
            Console.WriteLine("Bu Id-e uygun telebe tapilmadi..!");
        }
        public static void SaveData(Academy academy)//Fayla yazan 
        {
            try
            {

                var options = new JsonSerializerOptions { WriteIndented = true };


                string jsonString = JsonSerializer.Serialize(academy, options);


                File.WriteAllText("academy.json", jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Data yaddasa yazilarken xete bas verdi: " + ex.Message);
            }
        }

        public static Academy LoadData() //Fayldan oxuyan
        {
            string fileName = "academy.json";


            if (!File.Exists(fileName))
            {
                return new Academy("Step It Academy");
            }

            try
            {

                string jsonString = File.ReadAllText(fileName);

                Academy? academy = JsonSerializer.Deserialize<Academy>(jsonString);

                if (academy != null)
                {
                    return academy;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Data oxunarken xete bas verdi: " + ex.Message);
            }

            return new Academy("Step It Academy");
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {

            Console.Clear();
            Academy academy = new Academy("Step It Academy");
            academy = New_GTS.LoadData();
            Console.WriteLine($"{academy.Name} - a xos gelmisiz...");
            string? choice;
            Console.ReadKey();
            while (true)
            {
                Console.Clear();
                Console.Write("1. Akademiyaya elave etmek\n2. Akademiyaya baxmaq\n3. Akademiyada deyisiklik etmek\n4. Akademiyadan silmek\n5. Akademiyada axtarmaq\n0. Cixis etmek\nSecim edin: ");
                choice = Console.ReadLine();
                if (choice == "1")
                {
                    string? choice1;
                    while (true)
                    {
                        try
                        {

                            Console.Clear();
                            Console.Write("1. Qrup elave etmek\n2. Muellim elave etmek\n3. Telebe elave etmek\n4. Qrupa telebe elave etmek\n0. Cixis etmek\nSecim edin: ");
                            choice1 = Console.ReadLine();
                            if (choice1 == "1")
                            {
                                New_GTS.NewGroup(academy);
                                New_GTS.SaveData(academy);
                            }
                            else if (choice1 == "2")
                            {
                                New_GTS.NewTeacher(academy);
                                New_GTS.SaveData(academy);
                            }
                            else if (choice1 == "3")
                            {
                                New_GTS.NewStudent(academy);
                                New_GTS.SaveData(academy);
                            }
                            else if (choice1 == "4")
                            {
                                New_GTS.AddStudentGroup(academy);
                                New_GTS.SaveData(academy);
                                break;
                            }
                            else if (choice1 == "0")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Yalnis secim etdiniz.!");
                            }

                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.Message);
                            File.WriteAllText("Error.txt", ex.Message);
                        }
                        Console.ReadKey();
                    }

                }
                else if (choice == "2")
                {
                    string? choice1;
                    while (true)
                    {
                        try
                        {
                            Console.Clear();
                            Console.Write("1. Qruplara baxmaq\n2. Muellimler baxmaq\n3. Telebelere baxmaq\n0. Cixis etmek\nSecim edin: ");
                            choice1 = Console.ReadLine();
                            if (choice1 == "1")
                            {
                                New_GTS.ShowGroup(academy);
                            }
                            else if (choice1 == "2")
                            {
                                New_GTS.ShowTeacher(academy);
                            }
                            else if (choice1 == "3")
                            {
                                New_GTS.ShowStudent(academy);
                            }

                            else if (choice1 == "0")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Yalnis secim etdiniz.!");
                            }
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.Message);
                            File.WriteAllText("Error.txt", ex.Message);
                        }
                        Console.ReadKey();
                    }
                }
                else if (choice == "3")
                {
                    string? choice1;
                    while (true)
                    {
                        try
                        {
                            Console.Clear();
                            Console.Write("1. Qrupun adini deyismek\n2. Muellimin maasini deyismek\n3. Telebeni qiymetini deyismek\n0. Cixis etmek\nSecim edin: ");
                            choice1 = Console.ReadLine();
                            if (choice1 == "1")
                            {
                                New_GTS.UpdateGroup(academy);
                                New_GTS.SaveData(academy);
                            }
                            else if (choice1 == "2")
                            {
                                New_GTS.UpdateTeacher(academy);
                                New_GTS.SaveData(academy);
                            }
                            else if (choice1 == "3")
                            {
                                New_GTS.UpdateStudent(academy);
                                New_GTS.SaveData(academy);
                            }

                            else if (choice1 == "0")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Yalnis secim etdiniz.!");
                            }
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.Message);
                            File.WriteAllText("Error.txt", ex.Message);
                        }
                        Console.ReadKey();
                    }
                }
                else if (choice == "4")
                {
                    string? choice1;
                    while (true)
                    {
                        try
                        {
                            Console.Clear();
                            Console.Write("1. Qrupu silmek\n2. Muellimi silmek\n3. Telebeni silmek\n4. Qrupdan telebe silmek\n0. Cixis etmek\nSecim edin: ");
                            choice1 = Console.ReadLine();
                            if (choice1 == "1")
                            {
                                New_GTS.DeleteGroup(academy);
                                New_GTS.SaveData(academy);
                            }
                            else if (choice1 == "2")
                            {
                                New_GTS.DeleteTeacher(academy);
                                New_GTS.SaveData(academy);
                            }
                            else if (choice1 == "3")
                            {
                                New_GTS.DeleteStudent(academy);
                                New_GTS.SaveData(academy);
                            }
                            else if (choice1 == "4")
                            {
                                New_GTS.DeleteStudentGroup(academy);
                                New_GTS.SaveData(academy);
                            }
                            else if (choice1 == "0")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Yalnis secim etdiniz.!");
                            }
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.Message);
                            File.WriteAllText("Error.txt", ex.Message);
                        }
                        Console.ReadKey();
                    }
                }
                else if (choice == "5")
                {
                    string? choice1;
                    while (true)
                    {
                        try
                        {
                            Console.Clear();
                            Console.Write("1. Qrupu axtarmaq\n2. Muellimi axtarmaq\n3. Telebeni axtarmaq\n0. Cixis etmek\nSecim edin: ");
                            choice1 = Console.ReadLine();
                            if (choice1 == "1")
                            {
                                New_GTS.SearchGroup(academy);
                            }
                            else if (choice1 == "2")
                            {
                                New_GTS.SearchTeacher(academy);
                            }
                            else if (choice1 == "3")
                            {
                                New_GTS.SearchStudent(academy);
                            }

                            else if (choice1 == "0")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Yalnis secim etdiniz.!");
                            }
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.Message);
                            File.WriteAllText("Error.txt", ex.Message);
                        }
                        Console.ReadKey();
                    }
                }

                else if (choice == "0")
                {
                    Console.WriteLine("Akademiya sisteminden cixis etdiniz.!");
                    New_GTS.SaveData(academy);
                    break;
                }
                else
                {
                    Console.WriteLine("Yalnis secim etdiniz.!");
                }
                Console.ReadKey();

            }


        }
    }
}
