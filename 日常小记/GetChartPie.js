﻿function getChart() {
    var myChart = echarts.init(document.getElementById('chart'));
    var option = {
        backgroundColor: '#fff',
        graphic: {
            elements: []
        },
        series: [
            //{
            //    type: 'pie',
            //    radius: ['0%', '10%'],
            //    center: ['30%', '47%'],
            //    silent: true,
            //    label: {
            //        normal: {
            //            show: false,
            //        }
            //    },
            //    data: [{
            //        name: "环比",
            //        label: {
            //            normal: {
            //                position: 'center',
            //                show: true,
            //                textStyle: {
            //                    fontSize: '24',
            //                    fontWeight: 'bold',
            //                    color: '#000'
            //                }
            //            }
            //        },
            //        itemStyle: {
            //            normal: {
            //                color: 'transparent',
            //            }
            //        }
            //    }],
            //    animation: false
            //},
            {
                type: 'pie', //底色圆环
                radius: ['29.3%', '35.7%'],//控制最外部圆环大小
                center: ['30%', '25%'],//圆心位置
                silent: true,
                label: {
                    normal: {
                        show: false,
                    }
                },
                data: [{
                    itemStyle: {
                        normal: {
                            color: 'rgba(300,300,300,1)',//圆环颜色(这里由于同心圆，显示为控制的35%之外的圆环颜色)
                            shadowBlur: 10,
                            shadowColor: 'rgba(0,176,255,1)'//圆环的边界颜色
                        }
                    }
                }],
                animation: false
            },
            {
                name: 'chart1', //外部有颜色的圆环
                type: 'pie',
                radius: ['31%', '34%'],//控制代表35%那部分有颜色圆环的大小
                center: ['30%', '25%'],//圆心位置
                label: {
                    normal: {
                        show: false,
                    }
                },
                data: getData1() //计算圆环两部分比例，根据比例显示深度不同的颜色
                //,
                //animationEasingUpdate: 'quarticOut',
                //animationDurationUpdate: 2000
            },
            {
                z: -10,
                type: 'pie',//中心圆
                radius: ['0%', '28%'],
                silent: true,
                center: ['30%', '25%'],
                animation: false,
                clockwise: false,
                labelLine: {
                    normal: {
                        show: false
                    }
                },
                data: [{
                    value: 0,
                    name: '-35%',//中心圆显示的内容
                    label: {
                        normal: {
                            position: 'center',
                            show: true,
                            textStyle: {
                                fontSize: '24',
                                fontWeight: 'bold',
                                color: '#000'
                            }
                        }
                    },
                    itemStyle: {
                        normal: {
                            color: 'rgba(146,208,80,1)',//最后一位为透明度  中心圆的颜色
                            shadowBlur: 10,
                            shadowColor: 'rgba(300,300,300,1)'
                        }
                    }
                }]
            }
        ]
    };
    myChart.setOption(option);
}
var percent1 = Math.abs(-35) / 100;//计算百分比
function getData1() { //根据比例计算颜色
    var g = Math.round(255 - 255 * percent1),
        b = Math.round(255 - 255 * percent1);
    var mainColor = 'rgb(0,' + b + ',' + 255 + ')';
    var borderColor = 'rgb(0,' + Math.round(b - b * 0.1) + ',' + 255 + ')';
    return [{
        value: percent1,
        itemStyle: {
            normal: {
                color: mainColor,
                //color: 'rgba(146,208,80,1)',
                borderWidth: 1.5,
                borderColor: borderColor
            }
        }
    }, {
        value: 1 - percent1,
        itemStyle: {
            normal: {
                color: 'transparent'
            }
        }
    }];
}