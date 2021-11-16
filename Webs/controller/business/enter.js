(function (parent, enter) {
    /**
     * @description 正在执行操作
     */
    var _doing = false;

    /**
     * @description 数据源
     */
    var _dataSource = [];

    /**
     * @description 区域数据源
     */
    var _areaDataSource = [];

    /**
     * @description 字典数据
     */
    var _dictionaryDataSource = {
        /**
         * @description 来访事由
         */
        reason: undefined
    };

    /**
     * @description 引导人数据源
     */
    var _enterGuideDataSource = [];

    /**
     * @description 设备工具数据源
     */
    var _deviceToolsDataSource = [];

    /**
     * @description 性别数据源
     */
    var _genderSource = [];
    
    /**
     * @description 记录新增的列id
     */
    var _addArr = [];

    /**
     * @description 页面加载
     */
    $(document).ready(function () {
        initDataSource();
        initUI();
    });

    /**
     * @description 读取身份证件
     * @param {Object} cert 身份证件信息 
     */
    enter.readCard = function (cert) {
        if (_doing === true) {
            return;
        }
        var myCert = {
            VisitOrgization: "",
            Phone: "",
            CertID: cert.certNumber,
            CertTypeCode: "IdentityCard",
            CertTypeName: "身份证",
            Name: cert.partyName,
            Gender: cert.gender,
            Nation: cert.nation,
            BornDay: cert.bornDay.substr(0, 4) + "-" + cert.bornDay.substr(4, 2) + "-" + cert.bornDay.substr(4, 2),
            CertAddress: cert.certAddress,
            CertOrg: cert.certOrg,
            EffDate: cert.effDate.substr(0, 4) + "-" + cert.effDate.substr(4, 2) + "-" + cert.effDate.substr(4, 2),
            ExpDate: cert.expDate.substr(0, 4) + "-" + cert.expDate.substr(4, 2) + "-" + cert.expDate.substr(4, 2),
            IdentityPic: cert.identityPic,
            PicFront: cert.base64ID_PicFront,
            PicBack: cert.base64ID_PicBack
        };
        if (myCert.CertID.isNullOrEmpty()) {
            return;
        }
        for (var i = 0; i < _dataSource._data.length; i++) {
            if (_dataSource._data[i].CertID === myCert.CertID) {
                return;
            }
        }
        getHistoryVisitor(myCert);
    };

    /********************* UI begin **********************/

    /**
     * @description 初始化UI
     */
    function initUI() {
        /*var myCert = {
            VisitOrgization: "",
            Phone: "",
            CertID: "360124199402274514",
            CertTypeCode: "IdentityCard",
            CertTypeName: "身份证",
            Name: "游志洋",
            Gender: "男",
            Nation: "汉",
            BornDay: "1994-02-27",
            CertAddress: "江西南昌",
            CertOrg: "江西南昌",
            EffDate: "2017-01-01",
            ExpDate: "2027-01-01",
            IdentityPic: "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",
            PicFront: "",
            PicBack: ""
        };
        getHistoryVisitor(myCert);
        var myCert1 = {
            VisitOrgization: "",
            Phone: "",
            CertID: "360124199402274516",
            CertTypeCode: "IdentityCard",
            CertTypeName: "身份证",
            Name: "游志洋",
            Gender: "男",
            Nation: "汉",
            BornDay: "1994-02-27",
            CertAddress: "江西南昌",
            CertOrg: "江西南昌",
            EffDate: "2017-01-01",
            ExpDate: "2027-01-01",
            IdentityPic: "/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQH/2wBDAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQH/wAARCAB+AGYDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD+/CiiigAorzv4ofFn4cfBbwjqHjz4p+MdC8D+E9MCi61rxBfwafZiWVhHBbRvO6+bc3Mzx29tAmXmuJYoUBkkQH+Tv9v7/g4v8RWV54q+G/7H9hp+lWPmzWOnfGLVdPurnW5rOSzUnUtC8O6pa/YtMvre8fbay6xb30REDPcaZcRTCKOoRc5KKaT8/W36ibsru5/W94p8deC/BFo194y8W+G/CtosM1x5/iDWtO0hHhgR5Jni+3XEDTbEjckRB2O0gAkYr8Tfj1/wcJ/sHfCiOWy8D634q+Mmspqr6ZJJ4J0Vf+EehgjLiTW4PEOp3Nnpes6TujMdvLpFxdNemW3mtfMspHvIv4N/2gf2qfjj+0t4sHjP44fEzxV8R9eUTJaXPiTVJbq30xLhLdbiHR9OjWDTNIguvsdu93DpdnaRXVxGtxcJJOWkb5vn1KWJZjGTtkcMxzjnG3OO+Pc49c1k5VIVuTkjypq0m7pvTRx7XvvuvmKnVtUi3FWjJNOS54tpp2nC8eaL1Uo8yuvtLc/0KvAn/Bx1+wZrurrpfiq7+I3ha2khVhrF94NlntIrwyiIWIi0q5v5ZUbesy6jMLKERrMssELRRNd/ot8Ff+Cm/wCw/wDH3ULHRPAHx+8FnxDqd1JZ6Z4d8QahB4f1rUpobZryb7DZ38iC4jitUeeSWOQosUVwxbFrc+T/AJU3252ba+yQhg20sduV6c5JyCB3GB0rptL8S+K9KuLe/wBInmsHtJo7m3uLaZ0kt54HEsDxNtO1kkQOrKQVKgjkVVapJ1L8tON7c0aa5Yp2StFX0vu/O/odFarHEYiUo06VJS1cMPCVOjGySSpwnUqzitNeapN3d273P9h2zvrLUbdLvT7u1vrWQZjubO4iureQYByk0DvG4wQflY8EHvVqv86v9g//AILmftOfsyvp3h3xrq8fxD+H/m6bb3Ol+I71Te2ccc0q301lfNEoSW8hnDSy3i3XkzW4uUw0915v90H7Kf7afwE/a/8AB9n4j+EnjvQtc1VbC1ude8M217GdZ0OadGO27smbzkhlaK4NrJ8y3EUMk0Dz2yi4dKSfr2MZuMJqDnHnavy310t0+fQ+tKKKKoAooooAKxfEfiLRvCWg6v4m8Q30GmaHoVhc6nqmoXLBILSytYzLNNI7EKqqo6kgZIyQOa2q/Hj/AILhftRL+zR+w14vj0+9t7bxZ8XtTtPhr4chubS4uI7u31BWu/E3kz25H2G+tPD0F7e6fdzkW6XUEaSLOXS0uRuyuJuyb8j+WD/gsv8A8FJrr9q/4wz+C/A94w+G/g57nTNJsrW+1N7LUr6PyI08Q3VhOkdi13JC0otAsc7W6SyO0qTMsNv+G1p4d8R3myMW17qG9gqssEkjsWyAAsSYbPOc5x256+ofB3wXf/FP4qafYNaiU6lI9zcopLbY43gEhIYHOEJOeMcc1/Rh8Hf2PvhvpNpam60O2luY2gcSvaDzMoDjpKFGTgnGccAV8rm2dxyx+9PlqTjzx2V0mldarW/p9x9NkmQ186pc/K404uMJSgno7Rel/wDPU/nY8PfsqfFrxdGl3pXhad7ZtoAuEurd/mzj901uw6Dufwr1zwr/AME9/i34g1e3sNT0mbT4J87pEJIT5kHWW2CdCe4GR3AzX9a3hP4W+GtGtlisIUhK7QqrDgHAPcuxwOnJxjtxXfL4Ps93mSAmUH5cBcEd/m3HHr0Ga+YlxljKy5KUacm9qjk+fW3Z2+f4s+vo8AYWD5qlWtO28ZxTT2vovnr+h/O54J/4JF6Gtot14j1a8kmAQtD9l0+UHIJJyChJ3DA6EjPFd1qv/BNLwbpFq8VrYiaDggy2sSNgBsYRXJJxnp7ZxX74PosVspdBhcZIwQOcj1Ptz1rg/EVs8kMrYwRlc9u5X6cZHr614uL4pzilrfZq/vS/u29fu/U+hwfCWUxShLC091eo6a5tPV67enQ/kJ/ah/ZC1fwHc3114b064Fhayu7KkLRqFRid2FR/mCBjweuBg81p/wDBN/8Ab18c/sGftEeCviLZrcaj4Wtb5dH8e+FI2tbKfxD4X1DZBeWa3c1rLdxNZTfZNWjtIZ7JdQmsI9Oubq2truedP3/+O/g+z1fQNVF+nyXEE5BKK+cxzKMZYdCeuQOCM9q/lF/aI8FP4a+I+tR2C+VBb6hK0TBQpPlNEwDL8y4JXBHzDg9RX2PDOb4jMqUVVleqkptJtqyUW9X+VrbrY+A4w4fwmW1ZYmhBWUrQbjZczty2s9VpffbbTf8A1ef2X/2jfA37V/wT8G/HP4dx6hb+GPGVm09vZ6rEsWoWF1DtW80+6CfI09nMTbyyRZgkkRnt5JrcxTyfQFfyG/8ABrH+0D488d+H/j58GPEWtXeo+Gvh7pHhTxL4fs7m7edNOm8QalqFjd29pbt8tnZKLAPBbw4t0lmuniiieWYyf15V9yndJnw1NycIuStKyv69WrdHugoooplhX8dn/B03qutf25+y9ocN/dHRG8O+N9Ym0fzW+wS6tDrWjWNvqj25Oz7da2N1eWlvchfNjtry7gVxHcSq/wDYnX8qn/Bzf8E/FPiXwJ8DPjRpdlc3fh7wdca14L12a0hknbS28QXlpqdpfXgjjcQ2M0unx6f58jRqLu6tolEhlJhUk2rJXbt+aDl5tL28/n+Z/MT+xLcT6d8XPD17bAb3hmtiwJX5bh4I3GcHnac/41/VF4eutIsbVZL+4W3kXGWdo1TkMeGeQAdD6dPavwb/AOCd/wAGdI16RvGd9aib+zWWCDzoQEWS4t/OjfcSrBkkj3AZI65HGK/Qr4uXHxC1bU5PDHhpriG2eYwyXokaED5woMTPC8UilJGJYMfmBByM1+d8RQwOOzCjTq6+ypunU0TalzptavTz/Rn69wrLEZdlkZUoKp7ZxqWlfX3Urad9P6sz9HdP8a+E45oiNe0z5x8qre2gkxnG5kE24Kucs33R3wOK9X07UNB1ZP8AQ7+3vJmG5TayRXMZADEkSRzOv6c/jmvyR8IfspWdno8WtePvG2sazqk8tvPBYS/ZJ7TT1UtugCwSxow8xQ/mTxtIu47WAyT9t/CrTbDw1DFa6GwdyAihQEAXBBBKM46EcDgfz8xYPKqDUKUKnMtpez93Rx6p23tvrufbYPE4jF8rqwjT5kk4wva7trqk/lq/09Y8c+KdJ8L2lzLqEzRxwhnfYhd0VFdmOFYNhVBJPHqSK+A/Hv7afwz0lrrT7HU7y+uw5URpp10LdWUHCSzkhDKSyhBCZVIOSwPFfVvxLtnuprltTjDMUm3jlxghtwOSM4H069q+JPFPjD9l/wAJwTaX4/1vQNM1DUHkXyryC4ncyxkwSKy2nn+SI5JFWVpxEkOQ0xRfmojh8BOrFVaftY2vycvNfbpo/u+V9x42pUw/wu1uv/gN317qxxUfxx0n4oWU1tesLQMyxwwMSN0bBieJSGABOOnfJINfkT+2r8ENQstYufFFlZxzaPeLcT/aIyG2s8hWMS+WGVCVRjtZg2ATjvX6nD4c/DLV9RXUfAeqWDW7q0tomnPZzRtGwDowaO5n4ZQCpUncrbu9c38c/Bc1z8JvEmn6gGkjt7Oe63OPu/ZLS7kDYbAUAg5OcDqRgEH0cvxOEwOPUcPS9gnFx5OXl0fKmrN7X7aa6HzeaZdPM8DUliIvl9lKvT3esY3je9tn91u5gf8ABr6+pab+2v460+C4mg0zUfhd4hN9ZRTulpdvprWbaZLcW6t5d1NZm81AW8kqu9it1OsDRrqFwJf76q/jY/4NpvAvgPwD4m+KnxM8X6hp1j4n8eR2/g7wAmqSW8N3HHp6rf6z/Z8rOskaaxG7LcWu4i9j0a2vJIGTTFuLX+yevvcJiIYik5wlGXLNwkou/JJWvGSu3F9bPo0+p+N47CVsJUhCtSq0/aUoVabqQcVUpyvadNtJShe65o3V01fQKKKK6jiCvib/AIKH/DTwB8Xf2Sfin4E+I2E0XV7C0Onz5lBtfEdvcrLod1H5DJO0sN2A0KRMGkl2RsHjd42+2a+R/wBuDwpeeMf2ePFOm2do959i1Lw9rd1EmzctlpGqQ3d1Nh2RWEMaeYUBMj7dsSySlI2xxNWVHD1q0VeVOnOaSvvGLa216dNT0Mpw1LGZpl+Ervlo4nG4ajUbskoVKsYy+LTZ9T+SL9gnwjrXhDUfif8AC/xRYS6dqeg61p7i0kh8uMWh0ybypYcKkTwtKkihosxliAGLMSftr4j6N4k0+wdvDNhb3V8k0cMBmlaFEWR2DS7kB+aMEOFJwcYOOtdFo/hbStF+JltrWnRS/btc8HyWGrsYCqx/2Vem4s1DqxL7Y3MKhwCqHivc7aOPUFKSnhyOepHXjkgDPTt1r8mxM3WxtSvdydWfO+urcdPwP33DZV9RSwk48sYaYdW3pLRaPbVn5Hav+zl8fdd+Mfhvx7qvjLWtV8D2gsL/AFDwpc+JNWitTqMewXOjW9pY6laWDaJd3EJuRJLpVxfWqziCO9LRNPL+ifwi8Mav4Q062XV5Jp7iGTH2i5mknncSzSyKkkzne/lKyxKzMzFEXJJr6NtvDNulmsw+5BtC/IM9Sw6MPSuB1nxBZ3OuQ6Fa+e7oWNx+6byklicEAyAkdG7+/Fb3Pcw2GdJrSyX9fp8vmZHjRrTWdctY7iQ/vA/noPmDgTDdEeQxSRcq2PmweCK+JPip+wx4M+Lep2zeIre5vrLSNUudQtJLeG3s7iRrnURqUkc80EYeaOV1SGUSlnkhiVfMXqPrX4gi/wBHuTd2sayXILMqh2KtHuO85QE8AfT3FepfCTxXbeLPD7HhLy1k+z3sDBlMU8TTJIBvJYg7CysQu5SrAEMKxgnSqqSnKHaUVa239f8ADmOLoxruSa3/AMo20fofC+lfsr6P4V8RyeJdP+1WruQskEaNHAUSOK3iDxiTy3KRQrEkkiMyKCqnrnD+N2gWl94U8R6W8ZK3ul6laSRrGHLpPZXMDIRyWDLJtbjOGAznFfod4xnMEU6LzDyD8xAGN3qME4z3/E5r4i8bSpeaubUqrwO7Mx67xuUmNhkDbIPlOOcE9+nPNyWMVf2s6jV7zk+icX0/4JzV4yjhqdGz5Fai9f8Al0/i6u3XTboeKfs++Friw8IfALTvBb3CeLPDdz4SFnFsghkg1601hVtGRrsmFXnmYOzzbYkaZydsfT+0rQ/to0TRxqJc6gNL08X5k3eYbz7JD9qL78PvM+/dvAbdndzmv54/+CdnwAk8efGu98WanoMNn4J8ATw6zFhC9pqWtuNumWqieHZN9hO6/nNvcxXun3jaJdojws+P6MK/QOG6ElSxOLbdsVUjyq+jVNSTmvVycbvV8vkj8z8Scdl1SeSZXgFGUstwdSWJqKK5ozxLpOnQck7v2dOkqnK/h9qtW2wooor6Y/MQqhqmmWWs6de6VqVul1Yahby2t3byKGjmglUq6OrAqwIPRgynoykZBv0UmlJOLSaaaaaumno012aKjKUJRnCTjOElKMotqUZRd4yi1qmmk01qnqfhJ8cfgdr3we+IbhIzcaFqn2650LUI9whXTJpyGsJCWaVbiwjkigLyswu0McwfzmuLe28zRvs1wnlbvs5GQ2Npxk7SwB6DJDY+g61+9fxA+G/hP4m6N/YnivTheWySCe2ljkkgurW4VWCSwXMLRzxEbmV/LkQyRNLCzeVNKj/jr8efhpN8I/Gtx4chieTSLi3W/wBGu5PtDh7OYuGtzcys4uLi1kyJVyXihe1aTDSgD8+zfJ5ZfUliKCTwk5rl1vKlKSvyST+ze6hJdNHZ2b/Zsg4uq53Uw1HGTtjsNh+Wo5NKOJUJR/e0/wCWbjb2kOsuacbRbUOZi1tV02aBpQqeW2313hH2ADcDz37j0r4Y+Jms/EvTtcf/AIQSSwtby41JbqW4vrS7voJ7YK8M9o0FrqOmSRvMsiPFcm5kS3liV5LW4XMZ+jrzV1ggYM2CpGO2TyckHJPHPHHNeAeP/jH4I8JQXFxfah/xNom2pbKqPIfvbyFWYS4VgucR4G7sM14EJ1J1FGOt76W9P0/XzP0nD1liErWd7dne9tfv+R5J4xsv2gfFscKS+LLzwRHbTRzvd6D9l1a9uIoJDK+m4v4Xgjsr8AwXMgt2nSM74ZonAr6N/Z/k1Dw1BqEutXErXmqyi4uXfBJmFu0QHVS2Rgg18mt+1ULiOS8061vNQCo3+hpBdSb1wxY7I98uFC8kAAZra8C/tReHPE2qWuiz6Pr+ma3chmSFNB1IWAVMB913MFC4LLjI5BJ7VGKdSndz0S9dNunfVb6fia1qFv0aW+17K/Q+4fG2vxyWtwqyg7s+no3UEnkf/Wrw3wB8PdQ+LfxR8KeA9Nnhs73xJqYs7e9k+dYFjSS6ld4wybl8iCQIhkiWSYxxyT28btPGzxNrFwEO9iokUsuWyT8zA456eo6/WvrT/gmv4fm8T/tD3OvPp0WoaZ4R8J6pcT3z+Uz6XqeozW8GkSxje1xHJOsGoRiaNREIxLbzyj7VHDcLLaf13GYWhq41a1OE115Lp1Nrte4pa9Pi8z5HO8y+oYLG1rx9pRw1SdNSs17WyVNWej99xVut+9j9mf2fvgppXwI+H9n4OsbiPUb5p5L7WtXSAW51PUJid0pjGCIoVPkWay+ZPBZJb2ktxdfZlnk9xoor9ho0qdClTo0oqFOnFQhFX0S83q31berer1P58r16uJrVcRXm6lWtOVSpN7ylJ3bsrJLokkklZJWQUUUVoZBRRRQAV8J/t8+HbaX4VWHjctHDdeEtbs4ZZhbedcTWGtyDT2tVmDKbaEXclvdSNlkkeGKNo2YxyQ/dleB/tSaeNU/Z6+LdkYo5Xm8GaqsQlA2iUxqqsGPKHBILrghWYZwTXBmdKNbL8ZTlt9XqST7SpxdSMvlKKZ6WT154fNMBVhe6xVGLSdrwqTVOcevxQk1t1P539U11LlspKGhlyVOTzwR0UnjGe9eKaz8HvBHinXI/EOo6XbXWoxSGRJ5IS7hmdZfvebjlkUn5M8cYqfUU1TwtJJZai8r29mxSK4HzrsHXLIAoO7J5OD+ddX4U8V6FcJH5l38rYz8q98/9NQDnOOf5mvxxVcTGspUttLW035f+D+G5/Q2AxdPDcvPL1t/27vrfT8V6GHdeCtStbkNZ26R6QdyMFcp984X5GBJ+XPfiqsXhOz0jUhfQWqLcRrIqyldrYfhgH3Yy2PTnn1Ne8yeJPDKwFJLyFrcOrFJJIV5UkrlDNxjn1x+FeC/Er4p+EtOef7LfRRSJvKpDLH5YAz0ImP8AL2zVVpYuvfnV0/P/AAvv5rp3O3E5tTqJ8k39/kumvR7rV9jj/GXi+6jv1tJWDJEsiqpduAG9sAkk89+D6iv2c/4JCeHNMbwh8XvHASU61qHifSfD7ytNI0SaTaaXFqVvapCzGJPKvLy6l82JI5ZvPKXLTrBai3/nt03WL/x94hkv7dTPp0MxikmcsqnzArq0eVKyLtBG5Wxmv6O/+CTz6Tp3w++J/h2C8tzqaeLrDVn09HU3MenSaNa2cV5JEufLgmuYZoIXfaJpIJ1j3eRLs+l4VpUYY6i5WVXlquKdr39k/h1ve17abXR+bcYVJ1crxM03Zzo35bu8XWg3zW6XSvfTbQ/Weiiiv0s/IwooooAKK5Pxr488FfDfw/qHivx/4q0Dwd4a0u3lu9R1zxHqlppOm2ltAoeaaa6vJYo1SJDvkIJ2J8zYUE1+PH7R/wDwXk/Yq+B88+i+DL7xJ8c/E0VybR4PANlDD4dtw9o1xHfv4p1ubT9Kv7FJPKt7hNJlvbxJ5liFv+6vHtNKdGrVdqdOUvNLT5vZfNickt2l6n7Yu6xo0kjKiIrO7uQqIigszMzEBVUAlmJAABJOK/JP9rz9v/4c2vj8/sn/AA3+z/EDxtrGiajqPxI1HTL9X8P+AvDkELItvqt/aNJ52t6tevbWVjpNq4ulWSXUZmgtrMG5/mX/AGr/APgvF+1t8dLDxD4X8J6nonwT8Cas0kA0rwL5k/iptMmg8m406+8a3qxXcqSszyC60nS9DvYSsAguYzHO11+cP7Mn7XUPwZ+Jt14i1+G4l0nXU8jXb6w3XF+YI2UwoIGmgjmjizNMwDh9/Co5xgzLKcVUy3EqEkqlSDp8kHeSjNWl71mk2ny6J7t8ysjsynFYanj8PVrWcKNSNRKSXK5QlFxbTeqT19UvQ/p58S+GtP8AENjPbTF5EmJ3F48tyGzwW56mvhP4pfCTxborXMvgXWtTtnQv5NjEsMNu33sbm8t3GDwME8E5FfR/wh/a2/Z0+MFpbz+GfiBo8Utx5YXT9durLQtUjMgJ2TWOoXkUwcZBbyhIgBGWycV9E6lomk3Y+1Wl9ptzEwJingurWeKYE4DRSRyvHICeAVZiW+Xk1+SVMox2Fvz03Hld72ata2u39fl+r089wWIs4VL6Lqra200b1/T5H4T6n4W/afvZGgk1LUrdXJyY7pWQYz1ZrPB69cjP0rqPBv7P/j/WruCfxr4k1W7Jx51g6281vJuI3bnEMb8YI44OSc5xX7FXuiw/Znypx0x5TZ6HA7c/lVbRPB9vNIl0fs4hU4dpHClC3IV1ZhtJA6HBx7Vxzo41/Ane/Xm8u3fba+iNlj8Mtpb26+n/AMkfNfgr4bWPhTSo7G0tlghiCKEVNqjaGx8pJzjPrjHUV7X8Mfi98Q/gj4tn1/4b38Fhqj6deQPb3sT3Gj6o6xSGyh1a0XCyRQ3DCRZYytxCrTRo5t7i6gn9M1jT/C9pFIbnVdLto14aaa9s4YUJBIWSWSdVRv8AYbaxAzkCvkX4tftC/AT4SW97eeJPH/h9ZbQSB7TTb621m/lAV90dvYaXNdXbyPjartEtujsnnzRI24aYXAZxKtCWHo1VVjJShUgpxUX7u04rSW+3mcmJzjLVCdLEtTpyXLOE1GUJJ9GpOz7+WjXQg+Mv/Bdz9v8A+APivSoPHHwS+Hmq+EI9Stvtmv6Fb6pe6brdgLtIp4Eu7LSZn8M3F7GZYrSS+N9PbSKL02N/AhtpP6Xv2L/20/g3+298ItK+KPwo1y3nm8qK28WeFbiSOLX/AAhry29vNeaRrOn+bJNazwi5gkXLSRS2tzZ3lrcXenX2nX97/nnfte/8FFIPjPZzeBPhdpV3pfgaKZXuNV1c3FvrGpTWsxltJ/sSKLSwRo5ZI5rfztQY7VZblOUr5H+AH7Wnx2/Zs8XweNvgp8UfFHw48QCS3a8udBvI/s2rR2ouFtrfV9MvYrvStVhtjdXElqNRsbk2kk0klr5UsjOf13IsvzOvhVHHJQrxslKXO3JJaud1e7bWt7I/Ns9zHKZSisHRo0VTb/gRUXNe7fnV7PlXw93fVrVf609FfxA/Av8A4OjPjx4L0u40/wCO3wE8DfGq5UFNO1bwt40vfhFewoiWiwfb0l8KfEi21BxsvjcyeTazXLXFmwkhazuTqZXqyyfME2lRUl/NGrSs/TmnGX3pHzyzDCtXdRx8nCd//JYtfifin8bv2tfjj8ddc1jX/i18U/GPjy91nxBqXim4tda1q5fQrbWtVmu57u50jwzatb+HNBiX7Zc29jYaHpWnadpOnyDTNKtbPTo4rVPmO68STSPK2QBM+4/NnBIIxweePWuS1G5kXd+POa5ee7lA4OPmx7d/b+tfTujB25fcSVrRSSstkktFY811JpN8ze27811fy+46bU9YPzMXztz91sDHr+PvXLT6j5sTsGBJBUD3+buCe/ABxn9KybuV35bByR29Say3mZCwA/iHr6itZ01HDtro/wBfK3XXYIzlZN/cXINb1axu45ba4khMe4K6NyoJ7ZDd+4H1A617B4e/aL+NHg0K/hX4s+OPDmwqfJ0fxDfWCfKPl2fZZ02Af3VCqe614jCxmyHx95R69m9c0txZRHI6YyeB7n3/AM968GWV4fGaVle6d3bXTbr5fl2O2ljq+Gdqbtb5dn0/W59QS/t7/tguGhP7Q/xVMBOOfFV47kc8mWQtMzc8OZN46hsgVy+pftjftJai5fUPjh8Tb5znJvfGOrXKkEnP7qa7eMn0bZkdQRzXzhcW6JESM9en1BrNjiEg+buR6+p9CPSoXDOXx2j/AOSry8/L+tD0aWd4ycknLst3+W3T8T17WfjX8TfEzM2veP8AxXqhfcHS71q+lVg27IKiYKwIJDKwweMjGc+aX2oX927SyTPNyOWbO1c98Y545+mMVVEKxZx2bHTHb61Zf/j2/wC+f5NXp4bLqGFSjSVkuyS7evb/ACscuNxVWrfnd7vX8NPxIl1ErEVdjuwMc/72e9SWd6Tkb+Dj/wBmrn7jp+H9Gqey7f7v+NejD4l8/wAmfPVf4kvl/wCko63zj/eH5/8A16KzKK2Mz//Z",
            PicFront: "",
            PicBack: ""
        };
        setTimeout(function () {
            getHistoryVisitor(myCert1);
        }, 3000);*/
        kendo.culture("zh-CN");
        //$("#dialogInternalStakeholderList").kendoMultiSelect({
        //    autoBind: true,
        //    autoClose: false,
        //    dataSource: _internalStakeholderDataSource,
        //    dataTextField: "DisplayText",
        //    dataValueField: "ID",
        //    noDataTemplate: "没有数据"
        //});
        $(".enter_visit input").each(function () { $(this).kendoTextBox({}) });

        $("#enterReasonCode").kendoDropDownList({
            autoBind: true,
            change: reasonChanged,
            dataSource: _dictionaryDataSource.reason,
            dataTextField: "Name",
            dataValueField: "Code",
            optionLabel: "",
            noDataTemplate: "没有数据"
        });
        $("#enterReason").kendoTextArea({
            enable: false,
            maxLength: 100,
            readonly: true,
            rows: 10
        });
        $("#enterArea").kendoMultiSelect({
            autoBind: true,
            autoClose: false,
            dataSource: _areaDataSource,
            dataTextField: "Name",
            dataValueField: "ID",
            optionLabel: "",
            noDataTemplate: "没有数据"
        });
        $("#enterDeviceToolsSelect").kendoMultiSelect({
            autoBind: true,
            autoClose: false,
            dataSource: _deviceToolsDataSource,
            dataTextField: "DeviceName",
            dataValueField: "DeviceID",
            optionLabel: "",
            noDataTemplate: "没有数据"
        });
        $("#enterDeviceTools").kendoTextArea({
            rows: 10,
            maxLength: 100
        });
        $(".enter_cert_reader input").each(function () {
            $(this).kendoTextBox({
                readonly: true
            })
        });
        $("#enterVistor").kendoGrid({
            batch: true,
            columns: [
                {
                    attributes: { class: "", title: "#: VisitOrgization === null ? '' : VisitOrgization #" },
                    field: "VisitOrgization",
                    template: "#: VisitOrgization === null ? '' : VisitOrgization #",
                    title: " 来访单位",
                    nullable: false,
                    width: 250
                }, {
                    attributes: { class: "", title: "#: Phone === null ? '' : Phone #" },
                    field: "Phone",
                    template: "#: Phone === null ? '' : Phone #",
                    title: "电话号码",
                    nullable: false,
                    width: 200
                }, {
                    attributes: { class: "center_column", title: "#: CertID === null ? '' : CertID #" },
                    field: "CertID",
                    template: "#: CertID === null ? '' : CertID #",
                    title: " 证件号码",
                    width: 250,
                    editable: function (dataItem) {
                        return isEdit(dataItem);
                    }
                }, {
                    attributes: { class: "center_column", title: "#: Name === null ? '' : Name #" },
                    field: "Name",
                    template: "#: Name === null ? '' : Name #",
                    title: "姓名",
                    width: 150,
                    editable: function (dataItem) {
                        return isEdit(dataItem);
                    }
                }, {
                    attributes: { class: "center_column", title: "#: Gender === null ? '' : Gender #" },
                    field: "Gender",
                    template: "#: Gender === null ? '' : Gender #",
                    title: "性别",
                    width: 100,
                    template: function (data) {
                        return "<div id=" + data.uid + ">"+data.Gender+"</div>";
                    },
                    editable: function (dataItem) {
                        return false;
                    }
                }, {
                    attributes: { class: "center_column", title: "#: Name === null ? '' : Name #" },
                    field: "Name",
                    template: function (data) {
                        if (data.IdentityPic.isNullOrEmpty()) {
                            return "<div class=\"photo_container\"></div>";
                        } else {
                            return "<div class=\"photo_container\"><img src=\"data:image/jpeg;base64," + data.IdentityPic + "\" /></div>";
                        }
                    },
                    title: "相片",
                    width: 100,
                    editable: function (dataItem) {
                        return false;
                    }
                }, {
                    command: "destroy",
                    title: "编辑",
                    width: 100,
                    //}, {
                    //    attributes: { class: "", title: "#: EffDate === null ? '' : EffDate + ' 至 ' + ExpDate === null ? '' : ExpDate #" },
                    //    field: "CertOrg",
                    //    template: "#: EffDate === null ? '' : EffDate + ' 至 ' + ExpDate === null ? '' : ExpDate #",
                    //    title: "有效期",
                    //    width: 200
                    //}, {
                    //    attributes: { class: "center_column", title: "#: Nation === null ? '' : Nation #" },
                    //    field: "Nation",
                    //    template: "#: Nation === null ? '' : Nation #",
                    //    title: "民族",
                    //    width: 100
                    //}, {
                    //    attributes: { class: "", title: "#: BornDay === null ? '' : BornDay #" },
                    //    field: "BornDay",
                    //    template: "#: BornDay === null ? '' : BornDay #",
                    //    title: "出生日期",
                    //    width: 120
                    //}, {
                    //    attributes: { class: "right_column", title: "#: CertAddress === null ? '' : CertAddress #" },
                    //    field: "CertAddress",
                    //    template: "#: CertAddress === null ? '' : CertAddress #",
                    //    title: "证件地址",
                    //    width: 150
                    //}, {
                    //    attributes: { class: "", title: "#: CertOrg === null ? '' : CertOrg #" },
                    //    field: "CertOrg",
                    //    template: "#: CertOrg === null ? '' : CertOrg #",
                    //    title: "发证机关",
                    //    width: 200
                }],
            dataSource: _dataSource,
            editable: {
                confirmation: false,
                mode: "incell",
                confirmDelete: "Yes"
            },
            groupable: false,
            navigatable: false,
            resizable: true,
            sortable: false,
            remove: function (e) {
                if (_addArr.indexOf(e.model.uid) > -1) {
                    _addArr.remove(e.model.uid);
                }
                var tempArr = _dataSource._data;
                for (var i = 0; i < tempArr.length; i++) {
                    if (tempArr[i].uid === e.model.uid) {
                        tempArr.remove(tempArr[i]);
                    }
                }
                if (tempArr.length > 0) {
                    var data = tempArr[tempArr.length - 1];
                    if (_addArr.indexOf(data.uid) > -1) {
                        var gender = $("#" + data.uid + "").data("kendoDropDownList").value();
                        setCertInfo(data.CertID, data.Name, gender);
                    } else {
                        setCertInfo(data.CertID, data.Name, data.Gender);
                    }
                } else {
                    setCertInfo();
                }
            },
            cellClose: function (e) {
                if (e.model.uid === _dataSource._data[_dataSource._data.length - 1].uid) {
                    if (_addArr.indexOf(e.model.uid) > -1) {
                        var gender = $("#" + e.model.uid + "").data("kendoDropDownList").value();
                        setCertInfo(e.model.CertID, e.model.Name, gender);
                    } else {
                        setCertInfo(e.model.CertID, e.model.Name, e.model.Gender);
                    }
                }
            },
            dataBound: function (e) {
                initGenderUI();
            }
        });
        $("#enterGuidePerson").kendoDropDownList({
            autoBind: false,
            dataSource: _enterGuideDataSource,
            dataTextField: "StaffName",
            dataValueField: "StaffID",
            optionLabel: "",
            noDataTemplate: "没有数据",
            text: "请选择引导人",
            index: -1
        });
        $("#enterSubmit").kendoButton();
        $("#enterClear").kendoButton(); 
        $("#enterReturn").kendoButton();
    }

    /**
     * @description 初始化性别下拉框
     */
    function initGenderUI() {
        for (var i = 0; i < _addArr.length; i++) {
            $("#" + _addArr[i] + "").kendoDropDownList({
                autoBind: true,
                dataSource: _genderSource,
                dataTextField: "Gender",
                dataValueField: "Gender",
                optionLabel: "",
                noDataTemplate: "没有数据",
                select: function (e) {
                    for (var j = 0; j < _dataSource._data.length; j++) {
                        if (e.sender.element.context.id == _dataSource._data[j].uid) {
                            _dataSource._data[j].Gender = e.dataItem.Gender;
                        }
                    }
                }
            });
            for (var j = 0; j < _dataSource._data.length; j++) {
                if (_addArr[i] == _dataSource._data[j].uid) {
                    if (_dataSource._data[j].Gender.isNullOrEmpty()) {
                        var gender = $("#" + _addArr[i] + "").data("kendoDropDownList").value();
                        _dataSource._data[j].Gender = gender;
                    } else {
                        var genderComponent = $("#" + _addArr[i] + "").data("kendoDropDownList");
                        genderComponent.select(function (dataItem) {
                            return dataItem.Gender === _dataSource._data[j].Gender;
                        });
                    }
                }
            }
        }
    }

    /********************* UI end **********************/

    /********************* Event begin **********************/

    /**
     * @description 提交回复
     */
    $("#enterSubmit").on("click", function (e) {
        submitVisit();
    });

    /**
     * @description 清空数据
     */
    $("#enterClear").on("click", function (e) {
        enter.clearForm();
    });

    /**
     * @description 返回
     */
    $("#enterReturn").on("click", function (e) {
        window.register.return();
        window.register.startScroll();
    });

    /**
     * @description 添加
     */
    $("#enterAdd").on("click", function (e) {
        enterAdd();
    });

    /********************* Event end **********************/

    /********************* Business begin **********************/

    /**
     * @description 初始化数据源
     */
    function initDataSource() {
        _genderSource = new kendo.data.DataSource({
            data: [
                { Gender: "男" },
                { Gender: "女" }
            ]
        });
        _dataSource = new kendo.data.DataSource({
            schema: {
                model: {
                    fields: {
                        VisitOrgization: { nullable: false, type: "string", validation: { maxlength: 50 } },
                        Phone: { nullable: false, type: "string", validation: { maxlength: 50 } },
                        CertID: { type: "string" },
                        CertTypeCode: { type: "string" },
                        CertTypeName: { type: "string" },
                        Name: { type: "string" },
                        Gender: { type: "string" },
                        Nation: { editable: false, type: "string" },
                        BornDay: { editable: false, type: "date" },
                        CertAddress: { editable: false, type: "string" },
                        CertOrg: { editable: false, type: "string" },
                        EffDate: { editable: false, type: "date" },
                        ExpDate: { editable: false, type: "date" }
                    },
                    id: "CertID"
                }
            },
            serverPaging: false,
            serverSorting: false,
            serverFiltering: false
        });
        _areaDataSource = new kendo.data.DataSource({
            error: function (e) {
                layer.open({
                    btn: "确定",
                    content: e.xhr.responseText,
                    icon: 2,
                    yes: function (index) {
                        //权限错误
                        if (e.xhr.statusText === "Unauthorized") {
                            window.location.replace("index.html");
                        } else {
                            layer.close(index);
                        }
                    }
                });
            },
            group: {
                field: "Type",
                dir: "asc",
                compare: function (a, b) {
                    if (a.items[0].Index === b.items[0].Index) {
                        return 0;
                    } else if (a.items[0].Index > b.items[0].Index) {
                        return 1;
                    } else {
                        return -1;
                    }
                }
            },
            requestEnd: function (e) {
            },
            requestStart: function (e) {
            },
            schema: {
                data: function (response) {
                    if (response !== null) {
                        if (response.errcode === 0) {
                            return JSON.parse(response.DataJSON);
                        } else {
                            layer.open({
                                btn: "确定",
                                content: response.errmsg,
                                icon: 2
                            });
                            return [];
                        }
                    }
                },
                model: {
                    fields: {
                        Name: { type: "string" }
                    },
                    id: "ID"
                }
            },
            serverPaging: false,
            serverSorting: false,
            serverFiltering: false,
            transport: {
                read: {
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    dataType: "json",
                    type: "post",
                    url: system.api.business.enter.getAreas,
                    xhrFields: {
                        withCredentials: true
                    }
                }
            }
        });
        _deviceToolsDataSource = new kendo.data.DataSource({
            error: function (e) {
                layer.open({
                    btn: "确定",
                    content: e.xhr.responseText,
                    icon: 2,
                    yes: function (index) {
                        //权限错误
                        if (e.xhr.statusText === "Unauthorized") {
                            window.location.replace("index.html");
                        } else {
                            layer.close(index);
                        }
                    }
                });
            },
            group: {
                field: "Name",
                dir: "asc",
                compare: function (a, b) {
                    if (a.items[0].Order === b.items[0].Order) {
                        return 0;
                    } else if (a.items[0].Order > b.items[0].Order) {
                        return 1;
                    } else {
                        return -1;
                    }
                }
            },
            requestEnd: function (e) {
            },
            requestStart: function (e) {
            },
            schema: {
                data: function (response) {
                    if (response !== null) {
                        if (response.errcode === 0) {
                            return JSON.parse(response.DataJSON);
                        } else {
                            layer.open({
                                btn: "确定",
                                content: response.errmsg,
                                icon: 2
                            });
                            return [];
                        }
                    }
                },
                model: {
                    fields: {
                        DeviceName: { type: "string" },
                        DeviceID: { type: "string" },
                        DictID: { type: "string" },
                        Name: { type: "string" },
                        Order: { type: "int" },
                    },
                    id: "DeviceID"
                }
            },
            serverPaging: false,
            serverSorting: false,
            serverFiltering: false,
            transport: {
                read: {
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    dataType: "json",
                    type: "get",
                    url: system.api.business.enter.getDeviceTools,
                    xhrFields: {
                        withCredentials: true
                    }
                }
            }
        });
        _dictionaryDataSource.reason = system.common.initDictionaryDataSource("来访事由", system.api.business.enter.getDictionaries, null, function (e) {
            //var data = JSON.parse(e.response.DataJSON);
            //var items = [];
            //for (var i = 0; i < data.length; i++) {
            //    items.push({ label: data[i].Name, value: data[i].Code });
            //}
            //$("#enterReasonCode").kendoCheckBoxGroup({
            //    items: items,
            //    layout: "horizontal"
            //});
        });
        _enterGuideDataSource = new kendo.data.DataSource({
            error: function (e) {
                layer.open({
                    btn: "确定",
                    content: e.xhr.responseText,
                    icon: 2,
                    yes: function (index) {
                        //权限错误
                        if (e.xhr.statusText === "Unauthorized") {
                            window.location.replace("index.html");
                        } else {
                            layer.close(index);
                        }
                    }
                });
            },
            schema: {
                data: function (response) {
                    if (response !== null) {
                        if (response.errcode === 0) {
                            return JSON.parse(response.DataJSON);
                        } else {
                            layer.open({
                                btn: "确定",
                                content: response.errmsg,
                                icon: 2
                            });
                            return [];
                        }
                    }
                },
                model: {
                    fields: {
                        StaffID: { type: "int" },
                        StaffName: { type: "string" },
                        StaffJob: { type: "string" },
                        StaffSex: { type: "string" },
                        StaffHireDate: { type: "date" },
                        StaffAddr: { type: "string" },
                        StaffCompany: { type: "string" },
                        StaffNumber: { type: "string" }
                    },
                    id: "StaffID"
                }
            },
            transport: {
                read: {
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    dataType: "json",
                    type: "get",
                    url: system.api.business.enter.getStaff,
                    xhrFields: {
                        withCredentials: true
                    }
                }
            },
            serverPaging: false,
            serverSorting: false,
            serverFiltering: false
        });
    }

    /**
     * @description 是否可编辑
     */
    function isEdit(dataItem) {
        if (_addArr.indexOf(dataItem.uid) > -1) {
            return true;
        }
        return false;
    }

    /**
     * @description 获取历史来访信息
     * @param {Object} e 
     */
    function getHistoryVisitor(myCert) {
        var data = {
            CertID: myCert.CertID
        }
        window.system.ajax.post(window.system.api.business.enter.getHistoryVisitor, data, function (data, status, xhr) {
            if (data.errcode === 0) {
                setCertInfo(myCert.CertID, myCert.Name, myCert.Gender);
                var visitorRecord = JSON.parse(data.DataJSON);
                if (visitorRecord != null) {
                    myCert.VisitOrgization = visitorRecord.VisitOrgization;
                    myCert.Phone = visitorRecord.Phone;
                }
                _dataSource._data.push(myCert);
            } else if (data.errcode === -2) {
                layer.open({
                    btn: "确定",
                    content: myCert.Name + data.errmsg,
                    icon: 2
                });
            } else {
                layer.open({
                    btn: "确定",
                    content: data.errmsg,
                    icon: 2
                });
            }
        })
    }

    /**
     * @description 新增用户判断是否离场
     * @param {Object} e 
     */
    function getIsLeave() {
        var promiseArr = [];
        var count = 0;
        for (var i = 0; i < _dataSource._data.length; i++) {
            if (_addArr.indexOf(_dataSource._data[i].uid) > -1) {
                var data = {
                    CertID: _dataSource._data[i].CertID,
                    Name: _dataSource._data[i].Name
                }
                promiseArr[count] = new Promise(function (resolve, reject) {
                    window.system.ajax.post(window.system.api.business.enter.getIsLeave, data, function (data, status, xhr) {
                        if (data.errcode == 0) {
                            resolve(data.DataJSON);
                        } else {
                            reject(data.errmsg);
                        }
                    });
                });
                count++;
            }
        }
        Promise.all(promiseArr).then(function () {
            submit();
        }).catch (function(error) {
            layer.open({
                btn: "确定",
                content: error,
                icon: 2
            });
        });
    }

    /**
     * @description 修改来访事由
     * @param {Object} e 
     */
    function reasonChanged(e) {
        if (e.sender.value() !== "Other") {
            $(".enter_visit #enterReason").data("kendoTextArea").value("");
            $(".enter_visit #enterReason").data("kendoTextArea").readonly(true);
            $(".enter_visit #enterReason").data("kendoTextArea").enable(false);
        } else {
            $(".enter_visit #enterReason").data("kendoTextArea").readonly(false);
            $(".enter_visit #enterReason").data("kendoTextArea").enable(true);
        }
    }

    /**
     * @description 提交访问信息
     */
    function submitVisit() {
        if ($("#enterReasonCode").data("kendoDropDownList").value().isNullOrEmpty()
            || (($("#enterReasonCode").data("kendoDropDownList").value() === "Other") && $("#enterReason").data("kendoTextArea").value().isNullOrEmpty())) {
            layer.open({
                btn: "确定",
                content: "请填写来访事由。",
                icon: 2
            });
            return;
        }
        if ($("#enterArea").data("kendoMultiSelect").value().length === 0) {
            layer.open({
                btn: "确定",
                content: "请选择访问区域。",
                icon: 2
            });
            return;
        }
        if ($("#enterGuidePerson").data("kendoDropDownList").value().isNullOrEmpty()) {
            layer.open({
                btn: "确定",
                content: "请选择引导人。",
                icon: 2
            });
            return;
        }
        if (_dataSource._data.length === 0) {
            layer.open({
                btn: "确定",
                content: "请扫描身份证。",
                icon: 2
            });
            return;
        }
        for (var i = 0; i < _dataSource._data.length; i++) {
            _dataSource._data[i].VisitOrgization = _dataSource._data[i].VisitOrgization.isNullOrEmpty()
                ? $("#enterVisitOrgization").val() : _dataSource._data[i].VisitOrgization;
            if (_dataSource._data[i].VisitOrgization.isNullOrEmpty()) {
                layer.open({
                    btn: "确定",
                    content: "请填写来访单位。",
                    icon: 2
                });
                return;
            }
        }
        for (var i = 0; i < _dataSource._data.length; i++) {
            if (_dataSource._data[i].Phone.isNullOrEmpty()) {
                layer.open({
                    btn: "确定",
                    content: "请填写手机号码。",
                    icon: 2
                });
                return;
            } else {
                var reg = /^[1][3,4,5,7,8,9][0-9]{9}$/;
                if (!reg.test(_dataSource._data[i].Phone.trim())) {
                    layer.open({
                        btn: "确定",
                        content: "请填写正确的手机号码。",
                        icon: 2
                    });
                    return;
                }
            }
            if (_dataSource._data[i].CertID.isNullOrEmpty()) {
                layer.open({
                    btn: "确定",
                    content: "请填写证件号码。",
                    icon: 2
                });
                return;
            } else {
                var reg = /^\d{6}(18|19|20)\d{2}(0\d|10|11|12)([0-2]\d|30|31)\d{3}(\d|X|x)$/;
                if (!reg.test(_dataSource._data[i].CertID.trim())) {
                    layer.open({
                        btn: "确定",
                        content: "请填写正确的证件号码。",
                        icon: 2
                    });
                    return;
                }
            }
            if (_dataSource._data[i].Name.isNullOrEmpty()) {
                layer.open({
                    btn: "确定",
                    content: "请填写姓名。",
                    icon: 2
                });
                return;
            }
        }
        var certIds = _dataSource._data.map((item) => {
            return item.CertID;
        });
        if (new Set(certIds).size != certIds.length) {
            layer.open({
                btn: "确定",
                content: "证件号码重复。",
                icon: 2
            });
            return;
        }
        getIsLeave();
    }

    /**
     * 提交
     */
    function submit() {
        var deviceToolsSelect = getToolsMessage();
        var data = {
            AuthCode: $("#enterAuthCode").val(),
            ReasonCode: $("#enterReasonCode").data("kendoDropDownList").value(),
            ReasonName: $("#enterReasonCode").data("kendoDropDownList").text(),
            Reason: $("#enterReason").val(),
            Areas: JSON.stringify($("#enterArea").data("kendoMultiSelect").value()),
            DeviceNTools: deviceToolsSelect,
            EnterGuide: $("#enterGuidePerson").data("kendoDropDownList").value(),
            VisitorList: _dataSource._data
        };
        _doing = true;
        $("#dialogMask").show();
        system.ajax.post(system.api.business.enter.upsertVisits, [data], function (data, status, xhr) {
            if (data.errcode === 0) {
                enter.clearForm();
                layer.open({
                    btn: "确定",
                    content: data.errmsg,
                    icon: 1
                });
            } else {
                layer.open({
                    btn: "确定",
                    content: data.errmsg,
                    icon: 2
                });
            }
        }, function (xhr, textStatus, errorThrown) {
            layer.open({
                btn: "确定",
                content: xhr.responseText,
                icon: 2,
                yes: function (index) {
                    layer.close(index);
                }
            });
        }, function () {
            _doing = false;
            $("#dialogMask").hide();
        });
    }

    /**
     * 拼接工具信息
     */
    function getToolsMessage() {
        var count = 0;
        var deviceToolsSelect = '';
        var deviceTools = $("#enterDeviceTools").val();
        var selectItems = $("#enterDeviceToolsSelect").data("kendoMultiSelect").dataItems();
        if (selectItems.length === 0) {
            deviceToolsSelect = deviceTools;
        } else {
            for (var i = 0; i < selectItems.length; i++) {
                if (selectItems.length - 1 === count) {
                    if (deviceTools.isNullOrEmpty()) {
                        deviceToolsSelect += selectItems[i].DeviceName;
                    } else {
                        deviceToolsSelect += selectItems[i].DeviceName + "、" + deviceTools;
                    }
                } else {
                    deviceToolsSelect += selectItems[i].DeviceName + "、"
                    count++;
                }
            }
        }
        return deviceToolsSelect;
    }

    /**
     * @description 添加
     */
    function enterAdd() {
        var myCert = {
            VisitOrgization: "",
            Phone: "",
            CertID: "",
            CertTypeCode: "IdentityCard",
            CertTypeName: "身份证",
            Name: "",
            Gender: "",
            Nation: "",
            BornDay: "",
            CertAddress: "",
            CertOrg: "",
            EffDate: "",
            ExpDate: "",
            IdentityPic: "",
            PicFront: "",
            PicBack: ""
        };
        _dataSource._data.push(myCert);
        _addArr.push(_dataSource._data[_dataSource._data.length - 1].uid);
        setCertInfo();
        initGenderUI();
    }

    /**
     * @description 设置身份信息
     */
    function setCertInfo(certId,certName,gender) {
        $("#enterCertCertID").val(certId);
        $("#enterCertName").val(certName);
        $("#enterCertGender").val(gender);
    }

    /**
     * @description 清空表单
     */
    enter.clearForm = function clearForm() {
        $("#enterAuthCode").val("");
        $("#enterReasonCode").data("kendoDropDownList").select(0);
        $("#enterReason").val("");
        $("#enterArea").data("kendoMultiSelect").value([]);
        $("#enterDeviceToolsSelect").data("kendoMultiSelect").value([]);
        $("#enterDeviceTools").val("");
        $("#enterVisitOrgization").val("");
        $("#enterGuidePerson").data("kendoDropDownList").index = -1;
        $("#enterGuidePerson").data("kendoDropDownList").text("请选择引导人");

        setCertInfo();
        //$("#enterCertNation").val("");
        //$("#enterCertBornDay").val("");
        //$("#enterCertCertAddress").val("");
        //$("#enterCertCertOrg").val("");
        //$("#enterCertEffDate").val("");
        //$("#enterCertExpDate").val("");

        _dataSource._data = [];
        _dataSource.read();
        _addArr = [];
    }

    /********************* Business end **********************/
}(window.business = window.business || {}, window.business.enter = {}));