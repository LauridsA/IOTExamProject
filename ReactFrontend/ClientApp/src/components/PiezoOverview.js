import React, { Component } from 'react';
export class PiezoOverview extends Component {
    static displayName = PiezoOverview.name;

    constructor(props) {
        super(props);
        this.state = { consoleData: [] };
        this.getMessages = this.getMessages.bind(this);
        this.postMessageSong = this.postMessageSong.bind(this);
        this.postMessageTrack = this.postMessageTrack.bind(this);
    }

    postMessageSong(e) {
        if (this.inputS.value == "") {
            this.inputS.value = "[Empty]"
        }
        fetch('api/piezo/postMqttMessage/song/' + this.inputS.value, {
            method: 'POST'
        })
    }

    postMessageTrack(e) {
        if (this.inputT.value == "") {
            this.inputT.value = "[Empty]"
        }
        fetch('api/piezo/postMqttMessage/track/' + this.inputT.value, {
            method: 'POST'
        })
    }

    getMessages() {
        fetch('api/piezo/GetMessageReactClient', {
            method: 'GET'
        })
            .then(response => {
                response.json().then(data => {
                    let datalist = [];
                    data.forEach(entry => {
                        let temp = {
                            message: entry['message'],
                            topic: entry['topic']
                        }
                        datalist = datalist.concat(temp)
                    })
                    console.log(datalist)
                    this.setState({
                        consoleData: datalist
                    });
                })
            })
    }

    render() {
        return (
            <div className='container'>
                <div className='row'>
                <div className='col-sm' >
                    <h1>Messages</h1>
                    <button className="btn btn-primary" onClick={this.getMessages}>Get the messages</button> <br />
                    <p>
                        <ul>
                            {
                                this.state.consoleData.map(item => <li style={{ listStyleType: "none", fontWeight: "bold", fontSize: "17px" }} key={item.topic}> {item.topic}: {item.message}</li>)
                            }
                        </ul>
                    </p>
                
                </div>
                <div className='col-sm'>
                    <h1>Player</h1>
                    <textarea type="text" placeholder="Play/Stop/Pause/Unpause" ref={(input) => this.inputS = input} /><br />
                    <button className="btn btn-primary" onClick={this.postMessageSong}>player</button> <br />
                </div>
                    <div className='col-sm'>
                        <h1>Track</h1>
                        <textarea type="text" placeholder="Next/Prev" ref={(input) => this.inputT = input} /> <br />
                        <button className="btn btn-primary" onClick={this.postMessageTrack}>song</button> <br />

                    </div>
                </div>
            </div>
        );
    }
}
