﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Battlefield_BitStream.Core.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Battlefield_BitStream.Core.Structs;
using Battlefield_2_BitStream.GameEvents;
using Battlefield_BitStream.Core.Networking;
using Battlefield_BitStream.Core.Helpers;
using VFS;
using Battlefield_BitStream.Core.Data;

namespace Battlefield_BitStream
{
    class Program
    {
        static void Main(string[] args)
        {
            Config.Initialize(args);
            var netServer = new NetworkingServer();
            /*Config.PlayingLevel = Level.LoadLevel("Strike_at_Karkand");
            Config.PlayingLevel.InitLevel();*/
            /*var fileName = VFileSystemManager.GetFileByName("ladder_4m.con");
            var files = VFileSystemManager.GetFilesByExtension(".con");
            int i = 0;
            foreach (var file in files)
            {
                Config.LoadedMod.GetConFileProcessor().ExecuteConFile(file);
            }*/
            DevelopmentHelper.DeconstructPacket("9ff1151f00000032013e78a38c9f090600008081a4780c62e6c485218fd11222888f05030040d0409c448db03ef3ad101b88cbb0c4278501000078a0cf6471f89a77640856db71c8e693c20000103c100ba31f1cd8112cb41e073664f1616100000820680f551c9eb2251992cf7a1ca2f8ec31000000141c0ca6097199f30add3b6a0d8b7cf6180000020a8808f48465658285c23c0f87443e7f0c0000410524df38c4aebfcf42315e2844331f40060080c0029dc71c62d7df6721da3d14228b8f200300007081db52cb505e0fae50ddc2e530c54790010020b84019c77018ce4f5c0824386b88c2b0cc000010648049f118c4cc890b431ea325440ccc0c00007c064844a0272c2b132c14e67938f4587c531800000010000000c0c0cc0000e06780711235c2faccb7426c202ec333b32b8600000000010000000c");
            DevelopmentHelper.DeconstructPacket("ef80141f0000002f01321c2387861800000041012c060e1c0eacea18020050150a00402b0400000008000000609001000018144054231fe1beaf8b217b649aa10000b4420000008000000000063807000082030c56f0157ebe8918daf92d1c0a00402b040000000800000060e87400002838c0e5308c61502d8e21231b1de10000b4410000008000000000864f070000830324b0221af6a7b2186a66741962187500003838c0990995615e8a8e21000041e1feff334300000080000000008653070000840324b052059ec4e51882eaa01c62407500004838c099c9a2e1e73b8b2179198aa10000b4c2000000800000000086560700008503cc76b41b8695e918b21c020f66787500005838c0182488e114ae8ba1912dc1a10000b4c20000008000000000065807000086032c5c9419c6cab9184ae1d61862");

            DevelopmentHelper.DeconstructPacket("9ff1151f00000032013e78a38c9f090600008081a4780c62e6c485218fd11222888f05030040d0409c448db03ef3ad101b88cbb0c4278501000078a0cf6471f89a77640856db71c8e693c20000103c100ba31f1cd8112cb41e073664f1616100000820680f551c9eb2251992cf7a1ca2f8ec31000000141c0ca6097199f30add3b6a0d8b7cf6180000020a8808f48465658285c23c0f87443e7f0c0000410524df38c4aebfcf42315e2844331f40060080c0029dc71c62d7df6721da3d14228b8f200300007081db52cb505e0fae50ddc2e530c54790010020b84019c77018ce4f5c0824386b88c2b0cc000010648049f118c4cc890b431ea325440ccc0c00007c064844a0272c2b132c14e67938f4587c531800000010000000c0c0cc0000e06780711235c2faccb7426c202ec333b32b8600000000010000000c");
            DevelopmentHelper.DeconstructPacket("9f01161f0000002d012e7c06660600803f038c85d10f0eec08165a8f031b5af10e2a0c0000000800000060606600000034c0f650c5e1295b9221f9acc72106660600804003ec33591cbee61d1982d5761c62a0660000e83740b7a596a1bc1e5ca1ba85cbe1d6ce73420000008000000000066a0600007f03d479cc2176fd7d16a2dd4321da4d762f040000000800000060a0660000f83740926f1c62d7df67a1182f14a2ba3f18c30000008000000000066a060000800354c6311c86f313170209ce1a62a06600000838c083c134212e735ea17b47ade149ed17c20000008000000000864e0700008103fc7e5a1d7a9358162a5c161c6e664e310c0000000800000060f87400001838c01affdfa16a7c65a1979ea06106720700808203cc761218b21c0e16ea51dd184a66662c040000000800000060");

            DevelopmentHelper.DeconstructPacket("6f200803000000cf001204840000000041010000043f1a003d484d3d2048616c6c2d4d616e6961204d6978205365727665720100e3003d484d3d2048616c6c2d4d616e6961204d697820536572766572202870726f043f7669646564206279206266326c2e6465297c2020202020202020202020202020202020202020202020202056656869636c653a204d6f202b20446f7c202020043f2020202020202020202020496e66616e747269653a204469202b204d69202b204672202b205361202b20536f7c7c20202020202020202020486f6d6570616700");
            DevelopmentHelper.DeconstructPacket("6f3008030000008d000e08043f653a20687474703a2f2f7777772e68616c6c2d6d616e69612e64657c2020202020202020202020205465616d737065616b333a203231322e3232342e38342e043f36383a39313334cf00000080a4eca36c03000000693a06000000060000001e000080110090000000c003000000a30200001900000019000060320000001000040600003200000020");
            DevelopmentHelper.DeconstructPacket("6f400803000000cf00120c84050000000d010000043f0a00000009006b756272615f64616d060067706d5f637140000c0064616c69616e5f706c616e74060067706d5f637140001100726f61645f746f5f6a616c61043f6c61626164060067706d5f637140001000646171696e675f6f696c6669656c6473060067706d5f637140000c0067756c665f6f665f6f6d616e060067706d5f043f637140000d006d617368747575725f63697479060067706d5f637140000d007461726162615f717561727279060067706d5f6371400014006f70657261746900");
            DevelopmentHelper.DeconstructPacket("6f30100f0000003e000a08840100000030000000043009004c696665436f6465721ce5fa5d040000000000180045654a6635473469464f376f51424e6e5343444772545f5f01b2");
            DevelopmentHelper.DeconstructPacket("6f500c070000002001261005018303000000204c696665436f64657200000000000000000000000000000000000000000000b6948c822040000000001015313319369516b1373a991880460ae33f00001c83460ae33f00006c31428a426080000000001015313319369516b1373a99190000000000000000000000000000000000008082a0c0000000001015313319369516b1373a19180000000000000000000000000000000000008042e000010000001015313319369516b1373a191a00000000000000000000000000000000000080822041010000001015313319369516b137ba981c00000000000000000000000000000000000080426081010000001015313319369516b1373a19190000000000000000000000000000000000000055060000000000000004");
            DevelopmentHelper.DeconstructPacket("6f40141f0000001000060c2703323b833239ff040000000000");
            DevelopmentHelper.DeconstructPacket("6f60100f00000023011a14043f6f6e736d6f6b6573637265656e060067706d5f6371400010006f7065726174696f6e68617276657374060067706d5f637140001100737472696b655f61745f04116b61726b616e64060067706d5f63714000840000000041010000043f1a003d484d3d2048616c6c2d4d616e6961204d6978205365727665720100e3003d484d3d2048616c6c2d4d616e6961204d697820536572766572202870726f043f7669646564206279206266326c2e6465297c2020202020202020202020202020202020202020202020202056656869636c653a204d6f202b20446f7c202020043f2020202020202020202020496e66616e747269653a204469202b204d69202b204672202b205361202b20536f7c7c20202020202020202020486f6d6570616700");
            Console.ReadLine();
        }
    }
}