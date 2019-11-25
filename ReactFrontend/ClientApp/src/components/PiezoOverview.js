import React, { Component } from 'react';


export class PiezoOverview extends Component {
  static displayName = PiezoOverview.name;

  constructor (props) {
    super(props);
    this.state = { messages: [], topics: [] };
      this.getMessages = this.getMessages.bind(this);
  }

    getMessages () {
       fetch('api/piezo/GetMessageReactClient', {
         method: 'GET'
       })
       .then(response => {
        //callback
        response.json().then(data => {
          //we implement the logic here.
          data.forEach(entry => {
            this.setState({
              messages: this.state.messages.concat(entry['message']),
              topics: this.state.topics.concat(entry['topic'])
            })
          });
          console.log(this.state.messages)
          console.log(this.state.topics)
        })
      })
  }

  render () {
    return (
      <div>
        <h1>Messages</h1>
          
          <button className="btn btn-primary" onClick={this.getMessages}>Get the messages</button>
          <div>
            <p> Messages: </p>
            <ul>
              {this.state.messages.map(item => {
              return <li key={item}>{item}</li>
              })}
            </ul>
          <p> With Respective Topics: </p>
          <ul>
              {this.state.topics.map(item => {
              return <li key={item}>{item}</li>
              })}
            </ul>
          </div>

      </div>
    );
  }
}
