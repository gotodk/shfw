jsondata = jsondata = {
    "title": "模拟演示标题",
    "nodes": {
        "demo_node_69": {
            "name": "起始点001",
            "left": 22,
            "top": 21,
            "type": "start round",
            "width": 24,
            "height": 24,
            "alt": true
        },
        "demo_node_70": {
            "name": "起始点002",
            "left": 130,
            "top": 23,
            "type": "start round",
            "width": 24,
            "height": 24,
            "alt": true
        },
        "demo_node_71": {
            "name": "结束结单",
            "left": 567,
            "top": 196,
            "type": "end round",
            "width": 24,
            "height": 24,
            "alt": true
        },
        "demo_node_72": {
            "name": "进行一个操作",
            "left": 76,
            "top": 122,
            "type": "task round",
            "width": 24,
            "height": 24,
            "alt": true
        },
        "demo_node_73": {
            "name": "进一步",
            "left": 167,
            "top": 117,
            "type": "plug",
            "width": 100,
            "height": 25,
            "alt": true
        },
        "demo_node_74": {
            "name": "管理涉设置流程",
            "left": 167,
            "top": 195,
            "type": "node",
            "width": 100,
            "height": 44,
            "alt": true
        },
        "demo_node_75": {
            "name": "开始返修",
            "left": 521,
            "top": 125,
            "type": "complex mix",
            "width": 100,
            "height": 25,
            "alt": true
        },
        "demo_node_84": {
            "name": "是否维修",
            "left": 341,
            "top": 122,
            "type": "fork",
            "width": 100,
            "height": 25,
            "alt": true
        },
        "demo_node_87": {
            "name": "自动提交",
            "left": 335,
            "top": 197,
            "type": "state",
            "width": 100,
            "height": 25,
            "alt": true
        }
    },
    "lines": {
        "demo_line_80": {
            "type": "sl",
            "from": "demo_node_69",
            "to": "demo_node_72",
            "name": ""
        },
        "demo_line_81": {
            "type": "sl",
            "from": "demo_node_70",
            "to": "demo_node_72",
            "name": ""
        },
        "demo_line_82": {
            "type": "sl",
            "from": "demo_node_72",
            "to": "demo_node_74",
            "name": ""
        },
        "demo_line_83": {
            "type": "sl",
            "from": "demo_node_74",
            "to": "demo_node_73",
            "name": ""
        },
        "demo_line_85": {
            "type": "sl",
            "from": "demo_node_73",
            "to": "demo_node_84",
            "name": ""
        },
        "demo_line_86": {
            "type": "sl",
            "from": "demo_node_84",
            "to": "demo_node_75",
            "name": ""
        },
        "demo_line_88": {
            "type": "sl",
            "from": "demo_node_84",
            "to": "demo_node_87",
            "name": ""
        },
        "demo_line_89": {
            "type": "sl",
            "from": "demo_node_87",
            "to": "demo_node_71",
            "name": ""
        },
        "demo_line_90": {
            "type": "sl",
            "from": "demo_node_75",
            "to": "demo_node_71",
            "name": ""
        }
    },
    "areas": {

    },
    "initNum": 91
};