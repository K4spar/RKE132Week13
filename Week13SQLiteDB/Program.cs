

using System.Collections.Specialized;
using System.Data.SQLite;
//insertCustomer(createConnection());
RemoveCustomer(createConnection());


static SQLiteConnection createConnection()
{
    SQLiteConnection connection = new SQLiteConnection("data source=mydb.db; version = 3; New = True; compress = True");
    try
    {
        connection.Open();
        Console.WriteLine("DB found");
    }  
    catch
    {
        Console.WriteLine("DB not found");
    }
return connection;
}

static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, * FROM customer";

    reader= command.ExecuteReader();

    while (reader.Read())
    {
        int readerRowid = reader.GetInt32(0);
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringDoB = reader.GetString(3);

        Console.WriteLine($"id: {readerRowid}. Full name: {readerStringFirstName} {readerStringLastName}; DoB {readerStringDoB}");
    }
    myConnection.Close();

}

static void insertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fName, lName, DoB;

    Console.WriteLine("Enter first name:");
    fName= Console.ReadLine();
    Console.WriteLine("Enter last name:");
    lName= Console.ReadLine();
    Console.WriteLine("Enter date of birth (mm-dd-yyyy):");
    DoB= Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"insert into customer(firstName, lastName, dateOfBirth)" +
        $"values ('{fName}', '{lName}', '{DoB}')";

    int rowInserted = command.ExecuteNonQuery();
    

    ReadData(myConnection);
Console.WriteLine( $"row inserted {rowInserted}");
}


static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    
    Console.WriteLine("Enter an id to delete a customer");
    string idToDelete = Console.ReadLine();

    command= myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";
    int rowDeleted = command.ExecuteNonQuery();
    Console.WriteLine($"{rowDeleted} was removed from the table customer");

    ReadData(myConnection);
}