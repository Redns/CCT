namespace Modem
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 待调制的比特流
            var bitStream = "101100001110";

            // BPSK调制/解调
            var bpskModulateResult = CCT.Modem.Modulator.BPSK(bitStream);
            var bpskDemodulateResult = CCT.Modem.Demodulator.BPSK(bpskModulateResult.i);

            // QPSK调制/解调
            var qpskModulateResult = CCT.Modem.Modulator.QPSK(bitStream);
            var qpskDemodulateResult = CCT.Modem.Demodulator.QPSK(qpskModulateResult.i, qpskModulateResult.q);

            // 2ASK调制/解调
            var baskModulateResult = CCT.Modem.Modulator.BASK(bitStream);
            var baskDemodulateResult = CCT.Modem.Demodulator.BASK(baskModulateResult.i);
        }
    }
}