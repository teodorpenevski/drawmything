# Draw My Thing

<p align="center"><img width="400" src="https://github.com/teodorpenevski/drawmything/blob/master/images/drawmything-logo2.png"/></p>

Изработено од: Христијан Пешов, Виктор Пановски и Теодор Пеневски

<h2>Опис на проблемот</h2>

<p align="justify">Играта <b>Draw My Thing</b> е визуелна мрежна игра наменета за минимум 2 играчи, при што најдобро е да се игра со максимум 4-5 играчи. Играта се состои од две улоги: цртач и погодувач/и. Целта на цртачот е да го нацрта(визуелно објасни) зборот, при што останатите го гледаат тој цртеж и соодветно треба погодат кој збор го црта. Зборот кој треба да се погодува се генерира случајно од листа на зборови од англискиот вокабулар. Исто така за секој играч се пресметуваат поени во зависност од тоа колку брзо го погодил зборот. Апликацијата е инспирирана од познатата веб игра <b>Sketchful.io</b>.</p>

<h2>Решение на проблемот</h2>

<p align="justify">Проблемот за играта е решен со користење на методи од multithreading, networking и синхронизација на нитките.</p>

<h3>Сервер/Server</h3>

<p align="justify">Голем дел од играта претставува мрежното поврзување помеѓу играчите, затоа е напишана цела класа која го претставува серверот кој се грижи за целосната организација на примање и испраќање на пораки. Исто така серверот чува информации за сите корисници за соодветно да може да испраќа ажурирања на податоците при некоја промена. Серверот е оној кој дава дозвола на играчот кој е на ред да црта.</p>
 

<h3>Порака/ClassToSend</h3>

<p align="justify">Пораките кои се праќаат помеѓу играчите и серверот претставуваат објекти од класата <b>ClassToSend</b>, кои се испраќаат серијализирани преку мрежа со BinaryFormatter, каде што на другата страна се десеријализираат и обработуваат податоците. Во оваа класа се наоѓаат сите информации кои ќе му бидат потребни на корисникот за соодветно да го испрати или прими и исцрта цртежот, или пак корисникот да го испрати зборот кој сака да го погоди. Се чуваат и дополнителни информации кои се потребни за почетната иницијализација на корисниците, ажурирање на поените, означување на крај на рундата, како и дали и кој корисник го погодил зборот.</p>
 
<h3>Корисник/User</h3>

<p align="justify">Корисникот е претставен со име, идентификатор и поените кои ги акумулирал. Потребно е да го чува и стримот кој претставува комуникација со серверот за да бидат овозможени сите функционалности. Постојат два типа на корисници, <b>ServerUser</b> и <b>RemoteUser</b>, кои наследуваат од класата <b>User</b>. <b>ServerUser</b> се користи да се претстават податоците на корисникот кои се чуваат кај серверот, при што разликата помеѓу <b>ServerUser</b> и <b>RemoteUser</b> е тоа што <b>ServerUser</b> чува податоци кои ќе помогнат за синхронизација на корисниците кај серверот и дополнително се чуваат поените на корисникот.</p>
 
<p><b>Код за класата User:</b></p>

```C#
[Serializable]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NonSerialized]
        public TcpClient Client;
        [NonSerialized]
        protected BinaryFormatter bf = new BinaryFormatter();
        public bool ConnectionClosed;

    }
```

<p><b>Код за класата ServerUser:</b></p>

```C#
[Serializable]
    public class ServerUser : User
    {
        [NonSerialized]
        public Semaphore s;
        public int points { get; set; }

        public ServerUser(int id, string name, TcpClient client)
        {
            Id = id;
            Name = name;
            Client = client;
            points = 0;
            s = new Semaphore(1, 1);
            ConnectionClosed = false;
        }
        public void RunServer(BlockingCollection<ClassToSend> bc)
        {
            //Initialize
            try
            {
                while (true)
                {
                    ClassToSend msg = (ClassToSend)bf.Deserialize(Client.GetStream());
                    if (msg.Type == Type.Ping)
                    {
                        continue;
                    }
                    msg.Id = this.Id;
                    msg.Name = this.Name;
                    bc.Add(msg);
                }
            }
            catch (Exception e)
            {
            }
        }

    }
```

<p><b>Код за класата RemoteUser:</b></p>

```C#
[Serializable]
    public class RemoteUser : User
    {

        public RemoteUser(string Name, string Address, int port = 25565)
        {
            this.Name = Name;
            Client = new TcpClient(Address, port);
            ClassToSend msg = new ClassToSend();
            msg.Name = this.Name;
            bf.Serialize(Client.GetStream(), msg);
        }

        public ClassToSend RunClient()
        {
            return (ClassToSend)bf.Deserialize(Client.GetStream());
        }


    }
```

<h2>Упатство за користење</h2>

<h3>Креирање/приклучување на сервер – Main Menu</h3>

<p align="justify">При стартување на играта, корисниците се претставени со главното мени, во кое може да се креира нова игра или приклучи на веќепостоечка. Доколку се креира нова игра, потребно е да се внесе само името на корисникот кој ќе го хостира серверот, при што портата по default е поставена на 25565. Откако ќе се пополнат потребните полиња, за стартување на серверот треба да се кликне на копчето <b>Host Server</b>. Доколку пак се приклучува корисникот на веќепостоечки сервер, потребно е да си внесе име со кое ќе игра, како и IP адресата на хостот. Овој корисник треба да го притисне копчето <b>Connect to server</b>.</p>
 
![Main Menu](https://github.com/teodorpenevski/drawmything/blob/master/images/mainmenu.jpg)
<p align="center">Слика 1. Main Menu</p>

<h3>Старт на игра – Lobby</h3>

<p align="justify">Има два различни погледи на Lobby во зависност од тоа дали сте хост или се приклучувате на веќепостоечки сервер. Доколку корисникот е хост, се прикажува следниот поглед, на кој во левата страна е листата на корисници кои се моментално приклучени. Откако сите ќе се приклучат се кликнува на копчето <b>Start game</b> и играта започнува.</p>

![Host Lobby](https://github.com/teodorpenevski/drawmything/blob/master/images/host-lobby.jpg)
<p align="center">Слика 2. Host Lobby</p>

<p align="justify">Доколку се приклучува на веќепостоечки сервер, се прикажува следниот поглед, на кој исто така на левата страна се прикажува листата на корисници кои се моментално приклучени, при што на врвот се наоѓа хостот на серверот. Оваа листа на корисници се ажурира со секое приклучување на нов корисник.</p>
 
![Player Lobby](https://github.com/teodorpenevski/drawmything/blob/master/images/player-lobby.jpg)
<p align="center">Слика 3. Player Lobby</p>

<h3>Тек на игра – Drawing/Guessing</h3>

<p align="justify">Откако ќе се започне играта се прикажуваат следните погледи, при што на тој што црта му е прикажен зборот кој треба да го нацрта, додека пак кај тие што погодуваат, зборот е скриен и е прикажана само првата буква. На десниот дел од прозорецот има чат на кој корисниците може да разменуваат пораки, т.е. да го погодуваат зборот. Доколку корисникот прв го погоди зборот добива 20 поени, а секој нареден добива пола поени од претходниот. Доколку сите корисници го погодат зборот или пак помине предвиденото време за да се објасни зборот, тогаш следниот корисник кој е на ред добива дозвола за цртање, а останатите погодуваат. Помеѓу секоја рунда корисниците добиваат известување дека завршила рундата и ќе започне нова.</p>

![Drawer](https://github.com/teodorpenevski/drawmything/blob/master/images/drawer.jpg)
<p align="center">Слика 4. Drawer</p>

![Guesser](https://github.com/teodorpenevski/drawmything/blob/master/images/guesser.jpg)
<p align="center">Слика 5. Guesser</p>
