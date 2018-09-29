import * as React from 'react';
import * as katex from 'katex';

const MathDisplay = (props: { data: string }) => {

  const math = katex.renderToString(props.data);
  return (<p dangerouslySetInnerHTML={ {__html: math} }/>);
}

export class Input extends React.Component< {}, { text: string }> {
  constructor(props : {}) {
    super(props);
    this.state = {text: ''};
    this.updateText = this.updateText.bind(this);
  }

  updateText(event: any) {
    let newText = event.target.value as string;

    if (newText.split('')[newText.length - 1] === '-')
      newText += '----->'

    this.setState({text: newText});
  }

  render() {
    const text = this.state.text;
    return (
      <div 
        style={{
          textAlign: 'center',
          position: 'absolute',
          margin: 'auto',
          top: 0,
          right: 0,
          bottom: 0,
          left: 0,
          height: '100px',
        }}
        >
        <MathDisplay data={text}/>
        <input 
          style={{
            display: 'inline-block',
          }}
          type='text'
          value={this.state.text}
          onChange={this.updateText}/>
      </div>);
  }
}
