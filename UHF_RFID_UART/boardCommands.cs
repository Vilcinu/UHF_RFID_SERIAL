using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UHF_RFID_UART
{
    enum memoryTypes
    {
        ROM = 0,
        RAM = 1,
        NVM = 2
    }
    enum bankTypes
    {
        USER = 3,
        TID = 2,
        EPC = 1,
        RESERVED = 0
    }

    struct boardCommand
    {
        public String name;
        public String command;
        public int address;
        public int length;
        public memoryTypes acces;
        public bankTypes type;
    }
     
    partial class BoardCommands
    {
        int userMinAddress = 0x00;
        int userMaxAddress = 0x1FF;
        String[] names = { 
         "FW Version", "Reader ID","Query EPC"

        ,"Multi EPC","Read Power","Write Power"

        ,"Read TID Bank","Read EPC bank","Read USER bank","Read Reserved bank"

        ,"Write EPC Bank","Write USER Bank","Write Reserved bank kill"
        ,"Write access password","Write Reserved bank kill and access password",

        "Access password","Kill","Lock Mask"};

        String[] CMDs = { "V", "S", "Q", "U", "N", "R", "W", "P", "K" };
        boardCommand[] boardCmd;
        public BoardCommands()
        {
            boardCmd = new boardCommand[names.Length];
            fillArray();
        }
        void fillArray()
        {
            for (int n = 0; n < names.Length; n++)
            {
                boardCmd[n].name = names[n];
                if (boardCmd[n].name.ToLower().Contains("read"))
                {
                    boardCmd[n].command = CMDs[5];
                }
                if (boardCmd[n].name.ToLower().Contains("write"))
                {
                    boardCmd[n].command = CMDs[6];
                }
                if (boardCmd[n].name.ToLower().Contains("kill"))
                {
                    boardCmd[n].command = CMDs[8];
                }
                boardCmd[n].address = 0;
                boardCmd[n].length = 0;
                boardCmd[n].acces = 0;
                boardCmd[n].type = 0;
            }
            boardCmd[0 ].command = "V";
            boardCmd[1 ].command = "S";
            boardCmd[2 ].command = "Q";
            boardCmd[3 ].command = "U";
            boardCmd[4 ].command = "N";
            boardCmd[5 ].command = "N";
            boardCmd[14].command = "P";
            boardCmd[16].command = "L";
        }
        void getCmdByNumber(int number)
        {

        }
        public String getTypeByNumber(int n)
        {
            String Result =  bankTypes.RESERVED.ToString();
            switch (n)
            {
                case 0:
                    
                    break;
                case 1:
                    Result= bankTypes.EPC.ToString();
                    break;
                case 2:
                    Result= bankTypes.TID.ToString();
                    break;
                case 3:
                    Result= bankTypes.USER.ToString();
                    break;

            }
            return Result;
        }
        public String getCmdByName(String name)
        {
            for(int n=0;n<boardCmd.Length;n++)
            {
                if (boardCmd[n].name.ToLower().Contains(name.ToLower()))
                {
                    return boardCmd[n].command; 
                }
                
            }
            return boardCmd[0].command;
        }

        public String getNameByNumber(int n)
        {
            if (n > boardCmd.Length)
                return boardCmd[boardCmd.Length - 1].name;
            return boardCmd[n].name;
        }
        void setAddres(int address)
        {

        }
        void setData(String data)
        {

        }
        
        public int getLength()
        {
            return boardCmd.Length; 
        }

        public String makeCmd(String CMDName, int addres = 0, int sizeWords = 0, String data = "")
        {
            String ResultCmd = "\n";
            int count = 0;
            int type = 0;
            if(CMDName.Contains("USER"))
            {
                type = 3;
            }
            if(CMDName.Contains("TID"))
            {
                type = 2;
            }
            if(CMDName.Contains("EPC"))
            {
                type = 1;
            }
            if(CMDName.Contains("RESERVED"))
            {
                type = 0;
            }
            for (int n = 0; n < names.Length; n++)
            {
                if (names[n].Contains(CMDName))
                {
                    ResultCmd += boardCmd[n].command;
                    count = n;break;
                }
            }
            if(CMDName.Contains("Read") && sizeWords <= 0)
            {
                sizeWords = 1;
            }
            if (boardCmd[count].command.Contains("R"))
            {
                ResultCmd += type.ToString() + "," + addres.ToString() + "," + sizeWords.ToString();
            }
            if (boardCmd[count].command.Contains("W"))
            {                  
                ResultCmd += type.ToString() + "," + addres.ToString() + "," + getWordsCnt(data).ToString()+","+ data;
            }
            ResultCmd = ResultCmd + "\r";
            return ResultCmd;
        }

        int getWordsCnt(String inpText)
        {
            int Result;

            Result = inpText.Length / 4;
            if(inpText.Length%4 != 0) 
            {
                Result += 1;
            }
            return Result;
        }
        public int getMinAdr()
        {
            return userMinAddress;
        }
        public int getMaxAdr()
        {
            return userMaxAddress;
        }
    }
}
