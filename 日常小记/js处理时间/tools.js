//json格式的datetime转为js格式的datetime
function timeJson2Date(time) {
    if (time != null) {
        var date = new Date(parseInt(time.replace("/Date(", "").replace(")/", ""), 10));
        return date;
    }
    return null;
}
//json格式的datetime转为string字符串
function timeJson2String(time) {
    if (time != null) {
        var date = timeJson2Date(time);
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        return date.getFullYear() + "-" + month + "-" + currentDate;
    }
    return "";
}

//liunx格式的timestamp转为string
function timeStamp2String(time) {
    var datetime = new Date();
    datetime.setTime(time);
    var year = datetime.getFullYear();
    var month = datetime.getMonth() + 1 < 10 ? "0" + (datetime.getMonth() + 1) : datetime.getMonth() + 1;
    var date = datetime.getDate() < 10 ? "0" + datetime.getDate() : datetime.getDate();
    var hour = datetime.getHours() < 10 ? "0" + datetime.getHours() : datetime.getHours();
    var minute = datetime.getMinutes() < 10 ? "0" + datetime.getMinutes() : datetime.getMinutes();
    var second = datetime.getSeconds() < 10 ? "0" + datetime.getSeconds() : datetime.getSeconds();
    return year + "-" + month + "-" + date + " " + hour + ":" + minute + ":" + second;
}


// 对Date的扩展，将 Date 转化为指定格式的String
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

//调用： var time1 = new Date().Format("yyyy-MM-dd");var time2 = new Date().Format("yyyy-MM-dd HH:mm:ss");

//调用 var stime='2018-01-31';var t1=new Date(stime);t1.addDays(1); var t2=t1.Format("yyyy-MM-dd HH:mm:ss");//t2='2018-02-01'

Date.prototype.addHours = function (h) {
    this.setHours(this.getHours() + h);
};

Date.prototype.addDays = function (d) {
    this.setDate(this.getDate() + d);
};

Date.prototype.addWeeks = function (w) {
    this.addDays(w * 7);
};

Date.prototype.addMonths = function (m) {
    var d = this.getDate();
    this.setMonth(this.getMonth() + m);
    if (this.getDate() < d)
        this.setDate(0);
};

Date.prototype.addYears = function (y) {
    var m = this.getMonth();
    this.setFullYear(this.getFullYear() + y);
    if (m < this.getMonth()) {
        this.setDate(0);
    }
};

function ConvertCycleTime(F_Date, F_Cycle) {
    var str = F_Date.Format('yyyy-MM-dd hh:mm:ss');
    str = str.replace(/-/g, "/");
    var _date;
    if (F_Cycle == 3) {
        str = F_Date.Format('yyyy-MM-dd');
        str = str.replace(/-/g, "/");
        _date = new Date(str);
    }
    else if (F_Cycle == 4) {
        var WeekDay = F_Date.getDay();
        while (WeekDay != 1) {
            F_Date.addDays(-1);
            WeekDay = F_Date.getDay()
        }
        str = F_Date.Format('yyyy-MM-dd');
        str = str.replace(/-/g, "/");
        _date = new Date(str);
    }
    else if (F_Cycle == 5) {
        str = F_Date.Format('yyyy-MM-01');
        str = str.replace(/-/g, "/");
        _date = new Date(str);
    }
    return _date;
}

//获取url的某个参数，注意：参数部分不能包含#，否则会与页面内跳转的#混淆
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        return unescape(r[2]);
    return null;
}

var getRandomColor = function () {
    return '#' + (function (color) {
        //这个写法比较有意思,Math.floor(Math.random()*16);返回的是一个小于或等于16的数.然后作为0123456789abcdef的下标,这样每次就会得到一个这个字符串当中的一个字符
        return (color += '0123456789abcdef'[Math.floor(Math.random() * 16)])
            //然后判断这个新字符串的长度是否到6,因为16进制的颜色是由6个字符组成的,如果到6了,就返回这6个字符拼成的字符串,如果没有就执行arguments.callee(color)也就是函数本身.
            && (color.length == 6) ? color : arguments.callee(color); //将''字符串传给color
    })('');
}

/** 
* 获取本周、本季度、本月、上月的开始日期、结束日期 
*/
var now = new Date();                    //当前日期     
var nowDayOfWeek = now.getDay();         //今天本周的第几天     
var nowDay = now.getDate();              //当前日     
var nowMonth = now.getMonth();           //当前月     
var nowYear = now.getYear();             //当前年     
nowYear += (nowYear < 2000) ? 1900 : 0;  //    


var lastMonthDate = new Date();  //上月日期  
lastMonthDate.setDate(1);
lastMonthDate.setMonth(lastMonthDate.getMonth() - 1);
var lastYear = lastMonthDate.getYear();
var lastMonth = lastMonthDate.getMonth();

//格式化日期：yyyy-MM-dd     
function formatDate(date) {
    var myyear = date.getFullYear();
    var mymonth = date.getMonth() + 1;
    var myweekday = date.getDate();

    if (mymonth < 10) {
        mymonth = "0" + mymonth;
    }
    if (myweekday < 10) {
        myweekday = "0" + myweekday;
    }
    return (myyear + "-" + mymonth + "-" + myweekday);
}

//获得某月的天数     
function getMonthDays(myMonth) {
    var monthStartDate = new Date(nowYear, myMonth, 1);
    var monthEndDate = new Date(nowYear, myMonth + 1, 1);
    var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);
    return days;
}

//获得本季度的开始月份     
function getQuarterStartMonth() {
    var quarterStartMonth = 0;
    if (nowMonth < 3) {
        quarterStartMonth = 0;
    }
    if (2 < nowMonth && nowMonth < 6) {
        quarterStartMonth = 3;
    }
    if (5 < nowMonth && nowMonth < 9) {
        quarterStartMonth = 6;
    }
    if (nowMonth > 8) {
        quarterStartMonth = 9;
    }
    return quarterStartMonth;
}

//获得本周的开始日期     
function getWeekStartDate() {
    var weekStartDate = new Date(nowYear, nowMonth, nowDay - nowDayOfWeek);
    return formatDate(weekStartDate);
}

//获得本周的结束日期     
function getWeekEndDate() {
    var weekEndDate = new Date(nowYear, nowMonth, nowDay + (6 - nowDayOfWeek));
    return formatDate(weekEndDate);
}

//获得本月的开始日期     
function getMonthStartDate() {
    var monthStartDate = new Date(nowYear, nowMonth, 1);
    return formatDate(monthStartDate);
}

//获得本月的结束日期     
function getMonthEndDate() {
    var monthEndDate = new Date(nowYear, nowMonth, getMonthDays(nowMonth));
    return formatDate(monthEndDate);
}


//获得上月开始时间  
function getLastMonthStartDate() {
    var lastMonthStartDate = new Date(nowYear, lastMonth, 1);
    return formatDate(lastMonthStartDate);
}


//获得上月结束时间  
function getLastMonthEndDate() {
    var lastMonthEndDate = new Date(nowYear, lastMonth, getMonthDays(lastMonth));
    return formatDate(lastMonthEndDate);
}

//获得本季度的开始日期     
function getQuarterStartDate() {

    var quarterStartDate = new Date(nowYear, getQuarterStartMonth(), 1);
    return formatDate(quarterStartDate);
}

//或的本季度的结束日期     
function getQuarterEndDate() {
    var quarterEndMonth = getQuarterStartMonth() + 2;
    var quarterStartDate = new Date(nowYear, quarterEndMonth, getMonthDays(quarterEndMonth));
    return formatDate(quarterStartDate);
}

//增加日
var timeAddDays = function (stime, days) {
    var tes = new Date(stime);
    var eDate = new Date(tes.getFullYear(), tes.getMonth(), tes.getDate() + days);
    return eDate.Format("yyyy-MM-dd");
}

//增加年
var timeAddYears = function (stime, years) {
    var tes = new Date(stime);
    var eDate = new Date(tes.getFullYear() + years, tes.getMonth(), tes.getDate());
    return eDate.Format("yyyy-MM-dd");
}
