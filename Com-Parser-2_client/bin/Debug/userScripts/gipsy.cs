public class Script
{
	public static byte[] StartMarkPattern = new byte[] { 0xAA, 0xAA };
	public static bool ValidateByChecksum = true;
	
	public struct inData
	{
		public ushort startMark;
		public byte stage;
		public short time;
		public uint pressure;
		public ushort temperature;
		public short ax;
		public short ay;
		public short az;
		public short mx;
		public short my;
		public short mz;
		public short photo;
		public byte checksum;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
		public byte[] gps;
	};
	
	public struct outData
	{
		public ushort pt_dec_startMark;
		public byte pp_stage_plot0;
		public short pp_time_plot1;
		public uint pp_pressure_plot2;
		public float pp_temperature_plot3;
		public float pp_ax_plot4;
		public float pp_ay_plot5;
		public float pp_az_plot6;
		public short pp_mx_plot7;
		public short pp_my_plot8;
		public short pp_mz_plot9;
		public short pp_pt_dec_photo_plot10;
		public byte pt_hex_checksum;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
		public byte[] gps;
	};
	
	public static void ProcessData(object inputObj, out object outObj)
	{
		// обработка данных с заполнением структуры outData
		inData i = (inData)inputObj;
		
		outObj = new outData()
		{
			pt_dec_startMark = i.startMark,
			pp_stage_plot0 = i.stage,
			pp_time_plot1 = i.time,
			pp_pressure_plot2 = i.pressure,
			pp_temperature_plot3 = i.temperature / 100.0F,
			pp_ax_plot4 = i.ax / 32768.0F * 8.0F,
			pp_ay_plot5 = i.ay / 32768.0F * 8.0F,
			pp_az_plot6 = i.az / 32768.0F * 8.0F,
			pp_mx_plot7 = i.mx,
			pp_my_plot8 = i.my,
			pp_mz_plot9 = i.mz,
			pp_pt_dec_photo_plot10 = i.photo,
			pt_hex_checksum = i.checksum,
			gps = i.gps
		};
	}
}