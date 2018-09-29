import * as React from 'react';
import * as katex from 'katex';


const MathDisplay = (props: { data: string }) => {

  const math = katex.renderToString(props.data);
  return (<p dangerouslySetInnerHTML={ {__html: math} }/>);
}

const defaultState = {text: '', polynomial: [0], katex: ''};
type State = typeof defaultState;

export class Input extends React.Component< {}, State > {
  constructor(props : {}) {
    super(props);
    this.state = defaultState;
    this.updateText = this.updateText.bind(this);
  }
  
  updateText(event: any) {
    const input = event.target.value as string;

    const decimal = /(\d*\.?\d* ?)/g;
    
    const matches = input.match(decimal)
      .filter(match => match !== '' && match !== '.');

    const polynomial = matches
      .map(match => match === ' ' ? 1 : Number(match));

    const katex = polynomial
      .map((c, i) => { return {c, i}})
      .filter(pair => pair.c != 0)
      .map(({c, i}) => {
        const coefficient = `${c === 1 ? '' : c}`;
        const x_term =
          i === 0 ? '' :
          i === 1 
            ? 'x' 
            : `x^{${i}}`;
        return `${coefficient}${x_term}`;
      })
      .join(" + ");

    const text = matches.join('');
    this.setState({text, polynomial, katex});
  }

  render() {
    const {text, katex} = this.state;
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
        <MathDisplay data={katex}/>
        <input 
          style={{
            display: 'inline-block',
          }}
          type='text'
          value={text}
          onChange={this.updateText}/>
      </div>);
  }
}
