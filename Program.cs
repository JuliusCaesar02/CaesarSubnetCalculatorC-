using System;
using System.Text;

public class Sub
{
	int i = 0;
	class Analisi
	{
		public String ip;
		public String sm;
		public char classe;
		public int netBit;
		public int subBit;
		public int hostBit;
        public int[] netIpByte;
        public int[] broadIpByte;
	};
	Analisi indirizzo = new Analisi();

	public int[] decon(String IpSm)
	{
        String[] parts = IpSm.Split('.');
		int[] aux;
		aux = new int[4];
		for (int i = 0; i < 4; i++)
		{
			try
			{
				aux[i] = Int16.Parse(parts[i]);
				if (aux[i] > 255)
				{
					aux[0] = -2;
					return aux;
				}
			}
            catch (IndexOutOfRangeException) {
			aux[0] = -1;
			return aux;
		}
	}
		return aux;
	}
	public String setSm(String sm)
	{
		indirizzo.sm = sm;
		return indirizzo.sm;
	}
	public String setIp(String ip)
	{
		indirizzo.ip = ip;
		return indirizzo.ip;
	}
	public char setClasse()
	{
		if (decon(indirizzo.ip)[0] > 0 && decon(indirizzo.ip)[0] <= 126)
		{
			indirizzo.classe = 'A';
		}
		else if (decon(indirizzo.ip)[0] >= 128 && decon(indirizzo.ip)[0] <= 191)
		{
			indirizzo.classe = 'B';
		}
		else if (decon(indirizzo.ip)[0] >= 192 && decon(indirizzo.ip)[0] <= 223)
		{
			indirizzo.classe = 'C';
		}
		else if (decon(indirizzo.ip)[0] >= 224 && decon(indirizzo.ip)[0] <= 239)
		{
			indirizzo.classe = 'D';
		}
		else if (decon(indirizzo.ip)[0] >= 240 && decon(indirizzo.ip)[0] <= 255)
		{
			indirizzo.classe = 'E';
		}
		else
		{
			indirizzo.classe = 'X';
			return 'X';
		}
		return indirizzo.classe;
	}
	public int setByteCrit()
	{
		int aux = 0;
		for (int i = 3; i >= 0; i--)
		{
			if (decon(indirizzo.sm)[i] == 128 || decon(indirizzo.sm)[i] == 192 || decon(indirizzo.sm)[i] == 224 || decon(indirizzo.sm)[i] == 240 || decon(indirizzo.sm)[i] == 248 || decon(indirizzo.sm)[i] == 252 || decon(indirizzo.sm)[i] == 254)
			{
				aux = i + 1;
			}	
		}
		return aux;
	}
	public int setCidr()
	{
			int aux = 0;
			int aux2 = 0;
		for (i = 0; i < 4; i++)
		{
				aux = decon(indirizzo.sm)[i];
				while (aux != 0)
				{
					aux2++;
					aux &= aux - 1;
				}
			}
		return aux2;
	}
	public int setNetBit()
	{
		switch (indirizzo.classe)
		{
			case 'A':
				indirizzo.netBit = 8;
				return 8;
			case 'B':
				indirizzo.netBit = 16;
				return 16;
			case 'C':
				indirizzo.netBit = 24;
				return 24;
			default: return 1;
		}
	}
	public int setSubBit()
	{
		indirizzo.subBit = setCidr() - indirizzo.netBit;
		return indirizzo.subBit;
	}
	public int setHostBit()
	{
		indirizzo.hostBit = 32 - indirizzo.netBit - indirizzo.subBit;
		return indirizzo.hostBit;
	}
	public long setHostESubPerNet(int aux, int aux2)
	{
		return (long)(Math.Pow(2, aux)) - aux2;
	}
	public int setMn()
	{
		return 256 - decon(indirizzo.sm)[setByteCrit() - 1];
	}
	public int[] setNetIp()
	{
		indirizzo.netIpByte = new int[4];
		for (i = 0; i < 4; i++)
		{
			indirizzo.netIpByte[i] = decon(indirizzo.ip)[i] & decon(indirizzo.sm)[i];
		}
		return indirizzo.netIpByte;
	}
	public int[] setBroadIp()
	{
		int[] smComplement;
		indirizzo.broadIpByte = new int[4];
		smComplement = new int[4];
		for (i = 0; i < 4; i++)
		{
			smComplement[i] = ~decon(indirizzo.sm)[i] & 0xff;
			indirizzo.broadIpByte[i] = decon(indirizzo.ip)[i] | smComplement[i];
		}
		return indirizzo.broadIpByte;
	}
	public String setRange(int[] NetSubbyte, int importato)
	{
		int[] rangeAux;
		rangeAux = new int[4];
		for (i = 0; i < 3; i++)
		{
			rangeAux[i] = NetSubbyte[i];
		}
		rangeAux[3] = NetSubbyte[3] + importato;
			return string.Join(".", rangeAux);
		}
	public String setBitMap()
	{
		String aux = null;
		int j = 0;
		for (i = 1; i <= indirizzo.netBit; i++)
		{
			aux += "n";
			if (i % 8 == 0)
			{
				aux += ".";
			}
		}
		j = i;
		for (i = j; i <= indirizzo.subBit + indirizzo.netBit; i++)
		{
			aux += "s";
			if (i % 8 == 0)
			{
				aux += ".";
			}
		}
		j = i;
		for (i = j; i <= indirizzo.hostBit + indirizzo.subBit + indirizzo.netBit; i++)
		{
			aux += "h";
			if (i % 8 == 0 && i != 32)
			{
				aux += ".";
			}
		}
		aux += "";
		return aux;
	}
	public String setBinaryBit(int[] byteIPSM)
	{
		String aux = null;
		String aux2 = null;
		for (int j = 0; j <= 3; j++)
		{
				aux = Convert.ToString(byteIPSM[j], 2);
				if (aux.Length < 8 * (1 + j))
				{
				for (i = aux.Length; i < 8; i++)
				{
					aux = "0" + aux;
				}
			}
			aux2 += aux;
		}
		return aux2;
	}
	public String getBinaryBit(int[] byteIPSM)
	{
		StringBuilder aux = new StringBuilder();
		aux.Append(setBinaryBit(byteIPSM));
		for (i = 8; i < 32; i += 9)
			aux.Insert(i, ".");
		return aux.ToString();
	}
	public int setSubNet()
	{
		String aux = null;
		aux = setBinaryBit(decon((indirizzo.ip))).Substring(indirizzo.netBit, indirizzo.subBit);
		return Convert.ToInt32(aux, 2);
	}
}                    