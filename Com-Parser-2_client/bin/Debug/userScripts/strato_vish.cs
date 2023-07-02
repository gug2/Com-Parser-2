public class Script
{
	public static byte[] StartMarkPattern = new byte[] { 0xAA, 0xAA };
	public static bool ValidateByChecksum = true;
	
	const float Lsm6_A_Scale = 1.0F;
	const float Lsm6_G_Scale = 1.0F;
	const float Lsm303_A_Scale = 1.0F;
	
	public struct inData
	{
		public ushort startMark;
		public uint time;
		public ushort lsm6temp;
		public short lsm6gx;
		public short lsm6gy;
		public short lsm6gz;
		public short lsm6ax;
		public short lsm6ay;
		public short lsm6az;
		public short lis3mx;
		public short lis3my;
		public short lis3mz;
		public byte checksum;
		public short lsm303ax;
		public short lsm303ay;
		public short lsm303az;
		public byte lsm303mx_M, lsm303mx_L;
		public byte lsm303my_M, lsm303my_L;
		public byte lsm303mz_M, lsm303mz_L;
		public int ms56pres;
		public short ms56temp;
		public int eps1;
		public int eps2;
		public int eps3;
		public byte checksum_final;
	};
	
	public struct outData
	{
		public ushort pt_dec_startMark;
		public uint pp_pt_dec_time_plot0;
		public ushort pp_lsm6temp_plot1;
		public float pp_lsm6gx_plot2;
		public float pp_lsm6gy_plot3;
		public float pp_lsm6gz_plot4;
		public float pp_lsm6ax_plot5;
		public float pp_lsm6ay_plot6;
		public float pp_lsm6az_plot7;
		public short pt_dec_lis3mx;
		public short pt_dec_lis3my;
		public short pt_dec_lis3mz;
		public byte checksum;
		public float lsm303ax;
		public float lsm303ay;
		public float lsm303az;
		public short lsm303mx_MSB;
		public short lsm303my_MSB;
		public short lsm303mz_MSB;
		public int pp_ms56pres_plot8;
		public short pp_ms56temp_plot9;
		public int eps1;
		public int eps2;
		public int eps3;
		public byte pt_hex_checksum_final;
	};
	
	public static void ProcessData(object inputObj, out object outObj)
	{
		// обработка данных с заполнением структуры outData
		inData i = (inData)inputObj;
		
		outData o = new outData()
		{
			pt_dec_startMark = i.startMark,
			pp_pt_dec_time_plot0 = i.time,
			pp_lsm6temp_plot1 = i.lsm6temp,
			pp_lsm6gx_plot2 = i.lsm6gx / 32768.0F * Lsm6_G_Scale,
			pp_lsm6gy_plot3 = i.lsm6gy / 32768.0F * Lsm6_G_Scale,
			pp_lsm6gz_plot4 = i.lsm6gz / 32768.0F * Lsm6_G_Scale,
			pp_lsm6ax_plot5 = i.lsm6ax / 32768.0F * Lsm6_A_Scale,
			pp_lsm6ay_plot6 = i.lsm6ay / 32768.0F * Lsm6_A_Scale,
			pp_lsm6az_plot7 = i.lsm6az / 32768.0F * Lsm6_A_Scale,
			pt_dec_lis3mx = i.lis3mx,
			pt_dec_lis3my = i.lis3my,
			pt_dec_lis3mz = i.lis3mz,
			checksum = i.checksum,
			lsm303ax = i.lsm303ax / 32768.0F * Lsm303_A_Scale,
			lsm303ay = i.lsm303ay / 32768.0F * Lsm303_A_Scale,
			lsm303az = i.lsm303az / 32768.0F * Lsm303_A_Scale,
			pp_ms56pres_plot8 = i.ms56pres,
			pp_ms56temp_plot9 = i.ms56temp,
			eps1 = i.eps1,
			eps2 = i.eps2,
			eps3 = i.eps3,
			pt_hex_checksum_final = i.checksum_final
		};
		
		// swap bytes
		o.lsm303mx_MSB = (short)((i.lsm303mx_M << 8) | i.lsm303mx_L);
		o.lsm303my_MSB = (short)((i.lsm303my_M << 8) | i.lsm303my_L);
		o.lsm303mz_MSB = (short)((i.lsm303mz_M << 8) | i.lsm303mz_L);
		
		outObj = o;
	}
}