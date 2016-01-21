jsondata = {
    "title": "模拟演示标题",
    "nodes": {
        "demo_node_69": {
            "name": "起始点001",
            "left": 172,
            "top": 108,
            "type": "start round",
            "width": 24,
            "height": 24,
            "alt": true
        },
        "demo_node_70": {
            "name": "起始点002",
            "left": 292,
            "top": 109,
            "type": "start round",
            "width": 24,
            "height": 24,
            "alt": true
        },
        "demo_node_71": {
            "name": "结束结单",
            "left": 582,
            "top": 335,
            "type": "end round",
            "width": 24,
            "height": 24,
            "alt": true
        },
        "demo_node_72": {
            "name": "进行一个操作",
            "left": 203,
            "top": 201,
            "type": "task round",
            "width": 24,
            "height": 24,
            "alt": true
        },
        "demo_node_73": {
            "name": "进一步",
            "left": 376,
            "top": 147,
            "type": "plug",
            "width": 100,
            "height": 25,
            "alt": true
        },
        "demo_node_74": {
            "name": "管理涉设置流程",
            "left": 172,
            "top": 272,
            "type": "node",
            "width": 100,
            "height": 44,
            "alt": true
        },
        "demo_node_75": {
            "name": "开始返修",
            "left": 554,
            "top": 232,
            "type": "complex mix",
            "width": 100,
            "height": 25,
            "alt": true
        },
        "demo_node_84": {
            "name": "是否维修",
            "left": 367,
            "top": 237,
            "type": "fork",
            "width": 100,
            "height": 25,
            "alt": true
        },
        "demo_node_87": {
            "name": "自动提交",
            "left": 365,
            "top": 322,
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
            "name": "",
            "alt": true
        },
        "demo_line_81": {
            "type": "sl",
            "from": "demo_node_70",
            "to": "demo_node_72",
            "name": "",
            "alt": true
        },
        "demo_line_82": {
            "type": "sl",
            "from": "demo_node_72",
            "to": "demo_node_74",
            "name": "",
            "alt": true
        },
        "demo_line_83": {
            "type": "sl",
            "from": "demo_node_74",
            "to": "demo_node_73",
            "name": "",
            "alt": true
        },
        "demo_line_85": {
            "type": "sl",
            "from": "demo_node_73",
            "to": "demo_node_84",
            "name": "",
            "alt": true
        },
        "demo_line_86": {
            "type": "sl",
            "from": "demo_node_84",
            "to": "demo_node_75",
            "name": "",
            "alt": true
        },
        "demo_line_88": {
            "type": "sl",
            "from": "demo_node_84",
            "to": "demo_node_87",
            "name": "",
            "alt": true
        },
        "demo_line_89": {
            "type": "sl",
            "from": "demo_node_87",
            "to": "demo_node_71",
            "name": "",
            "alt": true
        },
        "demo_line_90": {
            "type": "sl",
            "from": "demo_node_75",
            "to": "demo_node_71",
            "name": "",
            "alt": true
        }
    },
    "areas": {
        "demo_area_63": {
            "name": "这是透明区域",
            "left": 127,
            "top": 82,
            "color": "blue",
            "width": 533,
            "height": 337,
            "alt": true
        }
    },
    "initNum": 91
};