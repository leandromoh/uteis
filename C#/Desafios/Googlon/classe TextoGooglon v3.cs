using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace testeGoogle
{
    public class PalavraMadeguesComparer : IComparer<PalavraMadegues>
    {
        public int Compare(PalavraMadegues x, PalavraMadegues y) => Compare(x.value, y.value);

        private static int Compare(string a, string b)
        {
            bool emptyA = String.IsNullOrEmpty(a), emptyB = String.IsNullOrEmpty(b);

            if (emptyA || emptyB)
                return emptyA ^ emptyB ? (emptyB ? 1 : -1) : 0;

            int posA = Array.IndexOf(PalavraMadegues.alfabeto, a[0]);
            int posB = Array.IndexOf(PalavraMadegues.alfabeto, b[0]);

            return posA != posB ? posA - posB : Compare(a.Substring(1), b.Substring(1));
        }
    }

    public class PalavraMadegues
    {
        public static char[] alfabeto { get; } = { 'j', 'n', 'g', 'm', 'c', 'l', 'q', 's', 'k', 'r', 'z', 'f', 'v', 'b', 'w', 'p', 'x', 'd', 'h', 't' };
        private static Regex regexPreposicao = new Regex("^[ac-z]{2}[vshz]$", RegexOptions.Compiled);
        private static Regex regexVerbo = new Regex("^[a-z]{6,}[bvshz]$", RegexOptions.Compiled);
        private static Regex regexVerboPrimeiraPessoa = new Regex("^[^bvshz][a-z]{5,}[bvshz]$", RegexOptions.Compiled);
        public string value { get; }

        public PalavraMadegues(string Value)
        {
            value = Value;
        }

        public static bool IsBeautifulNumber(long num) => num >= 787808 && num % 3 == 0;
        public long ToNumber() => value.Aggregate(new { Soma = (long) 0, Unidades = (long) 1 }, (acc, letra) => new { Soma = acc.Soma + Array.IndexOf(alfabeto, letra) * acc.Unidades, Unidades = acc.Unidades * 20 }).Soma;
        public bool IsPreposition => regexPreposicao.IsMatch(value);
        public bool IsVerb => regexVerbo.IsMatch(value);
        public bool IsVerbFirstPerson => regexVerboPrimeiraPessoa.IsMatch(value);
    }

    public class TextoMadegues
    {
        private IEnumerable<PalavraMadegues> palavras;

        public TextoMadegues(string Texto)
        {
            palavras = Texto.Split(' ').Select(s => new PalavraMadegues(s));
        }

        public int ContaPreposicoes() => palavras.Count(s => s.IsPreposition);
        public int ContaVerbos() => palavras.Count(s => s.IsVerb);
        public int ContaVerbosPrimeiraPessoa() => palavras.Count(s => s.IsVerbFirstPerson);
        public int ContaNumerosBonitos() => palavras.Count(s => PalavraMadegues.IsBeautifulNumber(s.ToNumber()));
        public string PegarListaDeVocabulario() => String.Join(" ", palavras.Distinct().OrderBy(s => s, new PalavraMadeguesComparer()).Select(s => s.value));

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