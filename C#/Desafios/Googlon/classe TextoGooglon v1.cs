using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Teste
{
    public class TextoMadegues
    {
        private static char[] ordemAlfabetica = { 'j', 'n', 'g', 'm', 'c', 'l', 'q', 's', 'k', 'r', 'z', 'f', 'v', 'b', 'w', 'p', 'x', 'd', 'h', 't' };
        private static char[] foo = { 'b', 'v', 's', 'h', 'z' };

        private string texto;
        private int verbo;
        private int verboPrimeraPessoa;
        private int numeroBonito;

        public TextoMadegues(string Texto)
        {
            texto = Texto;
        }

        public int ContaPreposicoes()
        {
            string pattern = " [ac-z]{2}(v|s|h|z) ";
            string textAux = " " + texto + " ";

            return Regex.Matches(textAux, pattern).Count;
        }

        public int ContaVerbos()
        {
            ContaTodosVerbos();
            return verbo;
        }

        public int ContaVerbosPrimeiraPessoa()
        {
            ContaTodosVerbos();
            return verboPrimeraPessoa;
        }

        public string PegarListaDeVocabulario()
        {
            string[] palavras = texto.Split(' ').Distinct().ToArray();

            Ordena(palavras);

            return String.Join(" ", palavras);
        }

        public int ContaNumerosBonitos()
        {
            numeroBonito = 0;
            string[] palavras = texto.Split(' ').Distinct().ToArray();

            foreach (string palavra in palavras)
            {
                long num = toNumber(palavra);

                if ((num >= 787808) && (num % 3 == 0))
                    numeroBonito++;
            }

            return numeroBonito;
        }

        private void ContaTodosVerbos()
        {
            verbo = 0;
            verboPrimeraPessoa = 0;

            string[] palavras = texto.Split(' ');

            foreach (string palavra in palavras)
            {
                if (palavra.Length >= 7)
                {
                    char primeiraLetra = palavra[0];
                    char ultimaLetra = palavra[palavra.Length - 1];

                    if (Array.IndexOf(foo, ultimaLetra) > -1)
                    {
                        verbo++;
                        if (Array.IndexOf(foo, primeiraLetra) == -1)
                            verboPrimeraPessoa++;
                    }
                }
            }
        }

        private static void Ordena(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (Comp(array[i], array[j]))
                    {
                        string aux = array[i];
                        array[i] = array[j];
                        array[j] = aux;
                    }
                }
            }
        }

        private static bool Comp(string a, string b)
        {
            if (String.IsNullOrEmpty(a) || String.IsNullOrEmpty(b))
                return (a.Length != 0);

            int posA = Array.IndexOf(ordemAlfabetica, a[0]);
            int posB = Array.IndexOf(ordemAlfabetica, b[0]);

            if (posA != posB)
                return posA > posB;
            else
                return Comp(a.Substring(1), b.Substring(1));
        }

        private static long toNumber(string str)
        {
            long soma = 0;
            long j = 1;

            for (int i = 0; i < str.Length; i++)
            {
                int algarismo = Array.IndexOf(ordemAlfabetica, str[i]);

                soma += algarismo * j;
                j *= 20;
            }

            return soma;
        }

        public static void Main(string[] args)
        {
            TextoMadegues textoA = new TextoMadegues("gmtm xnvgtlml sgcfmz tmw hbvcst mqnj gxr ltrkckj hzbpvj pgnfkhmd tcmpwnk phvx zpsqdhx bcdzdtmm npfbwlt xlwqbj wndfhdf fqx qpnxnsqk lxrcmgjc xlthlfdl xtdql vtznwg jhwd svshbq fmx hvpcwl srxx rhgjql fgx tjlx xzvcgpr gtg zpv sdp cjngcfv kcjpxdv jkvtpjnk zcjmvrbh lsnn gmlpkw nnsvhtff vlwtdch psbqbl rbptvg vpxmvkv rdgwtmj hjn pgh qkm lqpkj qjgmtrqf cdzbbckk fbhsb rpkdph ldk jxhft hrw tcmrdn vwf jjtrk dcdz xmczpt mfvmrs qqbphnnc hcmfkv bwqvdbzx zlsg bvsbdl mjspk ckdndgv sknrzck qvgnhq tsh nltpbns dcpp sbhstq lbkmzbh jmlwkllv cltww nxkwldm bdwn zlzn vhd zkxh xqlxknx xvwh wxhz bldcwc fqt lfmf mqb plvxk nnxplxl ktbdqfd cpxm lprbg tbq tkmvq ggzgtcfv qggswmr nkz xzg gwnfl wrhs mkwj gpggn gchp pfhl cwzrchv qtmxd xzprwf msrwws bwvf xwzbfq srvk kfqrdc ftqwk msrgfhw fmwm wtfgd pbp clmqlfwh hzzjn rmgbz gjzvfhzg jqgshslr nkflqc bjp rsxfnflp cmwvqjld pqqflg xrzrn xqgdnpw cjfzscq dzndcbm gwzgt fzmlvg gcm dbvcsbcv hdcr njk mlwl xsjbjtzz svt lgc xntlpmr mrjnhmgf swntq tdd qcsfvx jgslgvdf krztn nggpm pjdjrpn zqgrk mcsdh hvclhvk ftm khct znqfpcpw mjsmgssg lsxxhjwh lwcj gvwpbk zkxj rnhdtxgx ftmdzb cpcfhf jnd dqnpqw dwn jskpn qgbcrjzx mdmhtt kbbjthfl tlv lvhqnb ldht mpcxgz hcqgswp nqmkw cpfpdsn dbcbc mvqw vfjrlhj lxs wmglxhgv jbhstvvs bfq dvxlmr mjgjfgv fdrctz spdlgrf xwpcqgb kbqqhwb wkxgklq ddsgvxqx xfrcqt mvwf wdvrlbc hrmn cdxrkczw qlcsjk lqmgwq tjl glkqp zfsft vhfptfg dzkw qglltnd wcccwx bsdb zhddx spfkq swcxvbx qsznpfkr zslthtjv ljrltk vrvpzlfj mhzzrvs tqmndn pstqctwf kpzlgzg gtrq pdwffz dzx ktgnd fdk gwxwwngn lfjxb wbg glkgrs mmphjqf zgxllrxn sjzsttzg gpnlvb fxrct wnjp ntwvtzk xmvzjjs zcb zhkzrl kswskdjh lbtzh qsjh gzldc ftqpqqlw shwl wwdzlsc ztsbw jlfzqbpl bbqtzgrj rmrrgsp sqzk szhvwfw cxdv hwhpsdp drkp vlkzkc rpb kgfdhk lwbwxpzk tgdwwnsj ltcqs zblpzpv bvlp hdsnblpv dbq nlkz tmzjj qdrvhw sjmfn fpg cmmqz grwqhlh gjtbfws nwfxjpq qrk qspx dqhskm zphnxfn gspzcbrx przjqwr kfbh wmvjfwxr gfwb fcwntkz zpf cmcxptrh bqrxn lmppks wznjt dhxjnjlh hqdvpzw lgk qthd stn xwzkkx jvzmv dtp ppq zqbdl smphb kldknq tlrvbxsr zbjs khfhxcx hgfhv hrbbrbbs tggqvh cnvqd pjqxt mcvvw rht dzq ggsxpj mpvxms rbgwkbmc nvlkwzw wgp mwlkvdpq sfbq jtrzxqb xqdq zckjhmkc tsdm cthtzg fsbxxsk spcvh jhtr xphs nmzls fqnws mlnvq xdqlljf tflpsr ccgnwl rtxwqkt hmccs fnrqz lvksdrtr kqvpcvmr nbp kjgh clxzknq vkj qdfljlgp xtfl lpht zzlh mchgww gqwsvzlv zdbh hmgq xfb xcfn dkmkglqd zlvwjfmj dwj xgxdm fzxzmbz rfd lwzqsqsm dwz mnr pwdcwq rrmjdln rcg bfx bqrntz vpbrbg lmfqnw fclpdfh nxvhrh zfmrf wpr wbthbqs qdczwnl ssm dlbdshh fqvgks sssxx lsd cfbmmb dfxd lgq swh ntt vtbwbrc xch jxxq kzq wqcbpr xpk ggdqkp flkc bcng hgggdns dkqmcb zpm bqlqcp mbw fqp rdm zmq kjpgdp kpzcpsf gxtj jznbbfsr brm mwlg vnvdmtks lkpvtsd lmgjgv vqgzvf vjszvcrm svnttdtp fgsz mcjhb kssmc vrkgtpf slnttp qbmclrrd csdcm tqnqm qpvflxtq djhrq jrhwd qcznvsf txfqs sgzvqsnf xlkgwmlm qvxbr lhhb mcmpwjlt lzzrzp lvfqphvj slncfr fvmsqbhq jfkcdfxq wsjwnj clbpgx mjsh ccxvx bfxjbwj hxtmfqm tzpqtcnl ntz hbkg hpfd hzhdsgsc pgqktlpb jhrxc tbfrz hfpcwj sqxx wfdx rxcbt ckkk lmcfxjsn vzzvr zhmxdcxs mfrfftw ddh dpfxhkw hglhk gprmzpb xznlhrq brm jjdnztgs krvn qdr qqrzkdzr mzg fht dcbflwrh skrmqcc cxznmfdx hgpxwqg ngpdnpw tjnl qpc xtk sjg bjkhd frnnn qtjvnjs khnvgql chmrrd gzvn jtgsq clfbtqfm dml grqzx nhq gccjx wsdrk qkghs jhpvmv xnh smxb txzrh tzltk fnxhd rqqp slpzdvk fktc plfp slq szfsmnnr hmtntnn kbdfrh vgfzgqp nkkxlbwj fzlgv dszh bsrt jbnftt cbqgd jqbnd zbrxkrn rrd hzf cqpzvq vrmbzk wdlnhm zltthzp gnxqr qfwxhb bmmm wgnz mxb pztcvkg sdzpsj gsxfrnts jzlzscrz sdnz qpjxlqfk hshhzlhb xzvxsvq wbdgljv kdcgvs wpx qzpqg dxzgs xzbpzq rhkklr zldzwtk jrjfmmrt nvb tfm fnrvwzv bmpzqx pcjqgqcc ztms gcrrtftm wktcg tqggk dvdw cqz rzjjwk tmjh szbzxrz srwcxd rtswgvx wfd rkznd jcqcnnjl sstjg wsjj cdlkmfqx sfcs xgzd bqxs wvxslc rzrd pjbrmt gnxchpn skrfb kbxlz bnv szrvg czsbmn tslqcjtg qkk dmhn vskvskhc mfsgtj gxjgh ltqm tcdck qlgwpwhc lncg xts vlnw dqr zjbr qnbpk qpjmc");
            TextoMadegues textoB = new TextoMadegues("crwfss xqqqkk cbnkfsm lml bpvd xfzx gxdddr cbtng lsphxt rljqhs hfc kgqzcbxm xnglcwh cvkmgxt lgqzqtn kqmrthd vcglhg vzjkf tvvttj qfb zqr tgh ssth drlfzfd sbxrmlwc vwrpgd cfm kjfcfpw pjprz qst kzwt twgmjgww tvjlmgk gxr gksqkpp ldm djbr wsts lcfrgvx dgtnr xnhhgpf nxmcd jnjczv mnhj sjvfbnqt qwtmpzx wbkm wsjj crtbnzbp xsdt gkdrbdx fph bvkg psl hlqztsxr kdsscmwv vktfjk npwmk fkf bcss wsg snwp tbxjvsfm rztcp xddlzvd nfcdktbx txzkhlmj cgf msc sgtpnlg jxfp hzwtfx fmmkt dxphfdq xxl wqkfw dbhsj vlwdtrnv cnc qpj mglz wwxs rlcdn zhwxmncm mlgbms pgjzndvx tghggq fhqwdlm vjk dwpfkrj bqkhk lmjbvcz zkgrsz dkdv mtmx dgxwftxm cggl mznjhlm pqlx zcjgsv kmphtwvd mmbvn kctj kkn kfxh vlkq gqpr tvj wdcjntqq gqqvq mttnq lncrfvsd hvpt kqkmm trjdls kmgv rbvvjlqj hnldkc cbmcggvt xcgk sljvkbl nnnhsjqh sbsmxdlk fjhncdnf xcwg lpswbqzh ffqsx nnskjk pwlfq vdfxj cmzbf vgbls vtdxpkmd snbfn rcjjwdh kjmtpj vlcjn pmwnn wjhr lfl nnrcx plnksk fns ngj fknfr xsbdb lvbh xscbjvzr qtbjmdz jwbsz xvfvpxj wgjvzrq fqhmxvn lswbqdvc qbb vplwj ztdzq kvt ljgv kcgdz qdfs xcw sxtcqwxv vlv kdxzdk gnq gkxvv nmgzcb thn gpkthtc xgcqscj zqvbjtm fdzkl qkgsfnd qldcpk jtpzx sdbhmjsr tch pxg gggmz mdchn svflkdb qxmbcwpk drxrqmqq vpbqpj tjb gzwv ltpbg kddc fcsgh bvn clgx zlj hbmt zdrkwmzr szdtkrnm qzzjftnh fvbj lrntvzd wccx xclgfm bsn jwzh fknlgv rcd cqdvj lhkjmqql cvzkmr sxbvswd qjm xdvdn rsrfqvwg vczx qbjv trhh nxd twfqg sbrzbwdv clbnqz nrcnss kjtv mzcs bsp ttfzswss pkw xfbb ccxrbzl zrfcchsm vpdkl tsmh hxcbtkc tzj gqnrcr dtrbf bpcw dmfdpll fpj tggz njjxcv kfrrpzfr kfpmfsrb znks jpcwwxz msxxbxx nvvclxn vsgjkwz kkbchrps nrvl pvvjd wkgnzc szkl stvhsth smpqrpph ppprhl bzv bpxphx tswc ntd zvfj sshtv hfpqcw tkk jkqkdm fbdgjd zprx xzznvr btvvsj rttlqqkv hck fcqlt ffqsqf hccgv hjcrx hlkxpl nwzkc dcr pwkc gpzzm lvsxxzpc lgc fhnqvrl lcsl wqcqrj mqxzr qclxtsnf dlptthc cgb vmdd tns jmxbf tmch xfgqp bxmmbcd pjswnzr klxszcr vbrbbbf fggs fgx zwp hzh hmn zjrs wwrhxjj hqbgwh svkj frxjfxt crg bspwsh cdnnqmh nnwjbmfr ppr frx xmbnjgz brz jxklffp zbvkm sskdrw jkvjr bzgttxt rmr wkbwjpq qtww zckrbv mhwrcfrn gkscfg pgv rgxd xwrnkcnn drlpbnb kmfxstq hmb sbz wfmz ctcs stbjrb stpr qjw lszzfn wkldsmn fwh gtvtlrbl ckwmvsd flfdj sswczp vkff kvhvx lhgjdj jjghhjvr phh bvgsjn cxxjdhqz bhdqb lrbltq lwqdc dvrcv bbdvjcwb xfthjc wdcbxdlv zdvlpwmg fgqx hrl pzvgpks vzlgtst lnlgzqrm kkk dqvkbng xrplsln ckcthgxp jqgk tclkxs sfmnq hxsncgm qsj rlshbh njtnsghw gjs gflhwp mnqk mpnn hzmkcffq cqrjf bztjpj dsflvwrw kkrb tqgxmhcb wtzf wtbflncz lzwqzjdf ppnwgrbx clfwxhck ggw ptwpcpsj rrkpm bjk wdxxl ssvb bbqxs mblwgfw jkdh nrsqbp jdfvhvkk rnqthfb nznxtxsw vpfxlng wlxnssl jlp drclznbj fngnktq gxbvpz xhhkssb gkgl bgwvnm vthxc mxrjtpc rsj zdmhr zpgcwfh wxvzjc rvmqft kdhttxkv mdbjnszw cmxlwtn nknvkl hll bztjcmbd tbt jxsh mkxlkcq htqcmg mdhj rlw tlksbbgl trgqrmjv ngzrsj sjbzx znwcl qkmkwkd wxzxn ztfhgtj lspzp cmr jcbtj smcwp lvzqkg jjbrrjwb ffhvl zkmhhv rcdx jqjxfw lfcrqzst tmwdcmc zzqfgsrj bqhhhctp hxvdjb qzldwqc flmdt xjpxkp zmsjmtzp zlxhs rcdcmpn mrlvl mqztrz fngt xwmfhw thvjtmv rthss shnkdb pgspmlzp jrfvjr zwcns pbxrdfm svbbtm zmzngv vfqbkzx bvpngg dfrd bbntjn lvs ndbfln vvscmftx bmncplpz fbbn qvplcg wbfprq rrsvg qzd gmplsw hbqpzpj kbgkwtnb rmvbtlzs pgcsg vhl xwxdtbmr lvfl mnsvrx vxwlr sddqhwg tcnbcbk rtsvnxnk lbmksng lnsjkmvg mkfsqlzd rkklplsx vzgjf vpbj rpzfj znzmvvd zlcvhlq wsbkt dmswb fmd scrb wzx pkzp zrn mpvbjhq ndlzhf sgnz whs wcthf qmvxb lbklnx jdfr xwbfh kpmhg crw htmvzh tfqsqg rbq cdfczv flfnrvr wtksf dfmt vgkwzmvg tcczzqzg mdvzxsv dkd mqkgvgb gbx lwnp znr hjhq zhbtrwjp whwttmwk vxrmwql mvjnrfz hzgvqpwh glcws fbnf lfg qvtmx pfj gbjbhz fvskb dlqz jkrzvh gmpvnt qwtqd hxhpmj gsqs xrjpnzn tnfqcbz nqgr fvgw vprsbx dptkrnp zwk gspzkx mzkrtlk ffk vkpzx vkcwqc ztnkb sxmfzjdv jjhzq mwlf pjzgb cklbjwx xvv vcpdjxrg vht wsk swx qdgmgbqq ppgmrxlf pzzz ttx wnqclqv sgzzlz mbdcmhnc pnrxpn zwcgq gsgmtjb mkp zrrhwzms hclm bfmvwj ldwlslsb tgjnch kplk");
            
            Console.WriteLine(textoB.ContaPreposicoes());
            Console.WriteLine(textoB.ContaVerbos());
            Console.WriteLine(textoB.ContaVerbosPrimeiraPessoa());
            Console.WriteLine(textoB.ContaNumerosBonitos());
            Console.WriteLine(textoB.PegarListaDeVocabulario());
            Console.ReadKey();
        }
    }
}