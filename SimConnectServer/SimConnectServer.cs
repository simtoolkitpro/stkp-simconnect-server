using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SCServer
{
    public partial class SimConnectServer : Form
    {
        SimConnect simConnectService = null;
        const int WM_USER_SIMCONNECT = 0x0402;
        const int SC_BROADCAST_PORT = 51304;

        string[] arguments = Environment.GetCommandLineArgs();
        System.Net.IPEndPoint ip = null;

        CultureInfo Eng = CultureInfo.GetCultureInfo("en-GB");

        enum DEFINITIONS
        {
            Struct1,
        }

        enum DATA_REQUESTS
        {
            REQUEST_1,
        };

        // this is how you declare a data structure so that
        // simconnect knows how to fill it/read it.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        struct Struct1
        {
            // this is how you declare a fixed size string
            public int paused;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public String title;
            public double latitude;
            public double longitude;
            public double altitudemsl;
            public double fuel_total_weight;
            public double altitudeagl;
            public double ac_pitch;
            public double ac_bank;
            public double magvar;
            public double airspeed_true;
            public double airspeed_indicated;
            public double vspeed;
            public double heading;
            public double gforce;
            public double eng1_rpm;
            public double eng2_rpm;
            public double eng3_rpm;
            public double eng4_rpm;
            public double eng5_rpm;
            public double eng6_rpm;
            public int zulu_time;
            public int local_time;
            public int on_ground;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
            public String atcType;


        };

        public SimConnectServer()
        {
            InitializeComponent();
            if (arguments.Length == 1)
            {
                ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), SC_BROADCAST_PORT);
            } else
            {
                ip = new IPEndPoint(IPAddress.Parse(targetIp.Text), SC_BROADCAST_PORT);
            }
            SimConnectConnect();
        }

        private void SimConnectConnect()
        {
            try
            {
                simConnectService = new SimConnect("Managed Data Request", Handle, WM_USER_SIMCONNECT, null, 0);
                Console.WriteLine("SimConnect connection established");
                initDataRequest();
                Console.WriteLine("Data Request Initialised");

            }
            catch (COMException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void SimConnectDisconnect()
        {
            if (simConnectService != null)
            {
                // Dispose serves the same purpose as SimConnect_Close()
                simConnectService.Dispose();
                simConnectService = null;
                Console.WriteLine("Connection closed");
            }
        }

        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == WM_USER_SIMCONNECT)
            {
                if (simConnectService != null)
                {
                    try
                    {
                        simConnectService.ReceiveMessage();
                    } catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        if (debugMode.Checked)
                        {
                            debugOutput.AppendText("\r\n" + e.Message);
                        }
                    }
                }
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }


        // Set up all the SimConnect related data definitions and event handlers
        private void initDataRequest()
        {
            try
            {
                // listen to connect and quit msgs
                simConnectService.OnRecvOpen += new SimConnect.RecvOpenEventHandler(simconnect_OnRecvOpen);
                simConnectService.OnRecvQuit += new SimConnect.RecvQuitEventHandler(simconnect_OnRecvQuit);

                // listen to exceptions
                simConnectService.OnRecvException += new SimConnect.RecvExceptionEventHandler(simconnect_OnRecvException);

                // define a data structure
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "LOCAL TIME", "seconds", SIMCONNECT_DATATYPE.INT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "Title", null, SIMCONNECT_DATATYPE.STRING256, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "Plane Latitude", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "Plane Longitude", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "PLANE ALTITUDE", "Feet", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "FUEL TOTAL QUANTITY WEIGHT", "Pounds", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "PLANE ALT ABOVE GROUND", "Feet", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "PLANE PITCH DEGREES", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "PLANE BANK DEGREES", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "MAGVAR", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "AIRSPEED TRUE", "knots", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "AIRSPEED INDICATED", "knots", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "VERTICAL SPEED", "feet per minute", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "HEADING INDICATOR", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "G FORCE", "gforce", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "GENERAL ENG PCT MAX RPM:1", "rpm", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "GENERAL ENG PCT MAX RPM:2", "rpm", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "GENERAL ENG PCT MAX RPM:3", "rpm", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "GENERAL ENG PCT MAX RPM:4", "rpm", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "GENERAL ENG PCT MAX RPM:5", "rpm", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "GENERAL ENG PCT MAX RPM:6", "rpm", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "ZULU TIME", "seconds", SIMCONNECT_DATATYPE.INT32, 999999999.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "LOCAL TIME", "seconds", SIMCONNECT_DATATYPE.INT32, 999999999.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "SIM ON GROUND", "bool", SIMCONNECT_DATATYPE.INT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnectService.AddToDataDefinition(DEFINITIONS.Struct1, "ATC ID", null, SIMCONNECT_DATATYPE.STRING8, 0.0f, SimConnect.SIMCONNECT_UNUSED);


                // IMPORTANT: register it with the simconnect managed wrapper marshaller
                // if you skip this step, you will only receive a uint in the .dwData field.
                simConnectService.RegisterDataDefineStruct<Struct1>(DEFINITIONS.Struct1);

                // catch a simobject data request
                simConnectService.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(simconnect_OnRecvSimobjectDataBytype);
            }
            catch (COMException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void simconnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            switch ((DATA_REQUESTS)data.dwRequestID)
            {
                case DATA_REQUESTS.REQUEST_1:
                    Struct1 s1 = (Struct1)data.dwData[0];


                    bool engines_on = s1.eng1_rpm > 5 || s1.eng2_rpm > 5 || s1.eng3_rpm > 56 || s1.eng4_rpm > 5 || s1.eng5_rpm > 5 || s1.eng6_rpm > 5;
                    string e_on = engines_on ? "1" : "0";

                    string[] parts = new string[]
                    {
                    s1.latitude.ToString(Eng).Replace(",", ".").Replace("#",""),
                    s1.longitude.ToString(Eng).Replace(",", ".").Replace("#",""),
                    s1.altitudemsl.ToString(Eng).Replace(",", ".").Replace("#",""),
                    Math.Floor(s1.heading).ToString(Eng).Replace(",", ".").Replace("#",""), // TODO - Heading
                    s1.airspeed_indicated.ToString(Eng).Replace(",", ".").Replace("#",""),
                    s1.ac_pitch.ToString(Eng).Replace(",", ".").Replace("#",""),
                    s1.ac_bank.ToString(Eng).Replace(",", ".").Replace("#",""),
                    Math.Floor(s1.vspeed).ToString(Eng).Replace(",", ".").Replace("#",""), // TODO - Vspeed
                    "0",
                    Math.Round(s1.gforce, 2).ToString(Eng).Replace(",", ".").Replace("#",""), // TODO - Gforce
                    s1.altitudeagl.ToString(Eng).Replace(",", ".").Replace("#",""),
                    s1.on_ground.ToString(Eng).Replace(",", ".").Replace("#",""),
                    s1.paused.ToString(Eng).Replace(",", ".").Replace("#",""), /// Pause State
                    s1.title.ToString(Eng).Replace(",", "-").Replace("#",""),
                    "",
                    s1.airspeed_true.ToString(Eng).Replace(",", ".").Replace("#",""),
                    "",
                    s1.altitudeagl.ToString(Eng).Replace(",", ".").Replace("#",""),
                    s1.zulu_time.ToString(Eng).Replace(",", ".").Replace("#",""),
                    e_on,
                    s1.local_time.ToString(Eng).Replace(",", ".").Replace("#",""),
                    (s1.fuel_total_weight * 0.453592).ToString(Eng).Replace(",", ".").Replace("#",""),
                    s1.atcType.ToString(Eng).Replace(",", "-").Replace("#","")
                    };
                    string payload = $"{string.Join("#", parts)}";

                    var scdata = Encoding.UTF8.GetBytes(payload);
                    UdpClient client = new UdpClient();
                    client.Send(scdata, scdata.Length, ip);
                    client.Close();

                    if (debugMode.Checked)
                    {
                        debugOutput.AppendText("\r\n" + payload.Replace("#",", "));
                    }

                    Console.WriteLine(payload);
                    break;

                default:
                    Console.WriteLine("Unknown request ID: " + data.dwRequestID);
                    break;
            }
        }

        void simconnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            Console.WriteLine("Connected to Simulator");
        }

        // The case where the user closes Prepar3D
        void simconnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            statusPanel.BackColor = Color.DarkRed;
            simConnectService = null;
            Console.WriteLine("Simulator has exited");
            SimConnectDisconnect();
        }

        void simconnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            statusPanel.BackColor = Color.DarkRed;
            simConnectService = null;
            Console.WriteLine("Exception received: " + data.dwException);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SimConnectDisconnect();
            Application.ExitThread();
        }

        private void pulse_Tick(object sender, EventArgs e)
        {
            if (simConnectService != null)
            {
                pulse.Interval = 33;
                statusPanel.BackColor = Color.DarkGreen;
                try
                {
                    simConnectService.RequestDataOnSimObjectType(DATA_REQUESTS.REQUEST_1, DEFINITIONS.Struct1, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                }
                catch (COMException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } else
            {
                statusPanel.BackColor = Color.DarkRed;
                pulse.Interval = 2500;
                SimConnectConnect();
            }

        }

        private void trayIco_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void hideButton_Click(object sender, EventArgs e)
        {
            Hide();
            this.WindowState = FormWindowState.Normal;
        }

        private void SimConnectServer_Shown(object sender, EventArgs e)
        {
            if (arguments.Length >= 2)
            {
                Hide();
                WindowState = FormWindowState.Normal;
            }
        }

        private void debugMode_CheckStateChanged(object sender, EventArgs e)
        {
            if (debugMode.Checked)
            {
                debugOutput.Text = "";
            }
        }

        private void Reconnect(object sender, EventArgs e)
        {
            SimConnectDisconnect();
            try
            {
                ip = new IPEndPoint(IPAddress.Parse(targetIp.Text), SC_BROADCAST_PORT);
                SimConnectConnect();
            } catch (Exception ex)
            {
                MessageBox.Show("Unable to parse IP address");
            }
        }

        private void debugMode_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
