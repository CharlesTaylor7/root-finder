import * as React from 'react';
import { Latex } from './Latex';

const defaultState = { text: '', polynomial: [0], latex: '[\\ ]' };
type State = typeof defaultState;

const getState = (input: string) : State => {

  const decimal = /(\d*\.?\d* ?)/g;
      
  const matches = input.match(decimal)
    .filter(match => match !== '' && match !== '.');

  const polynomial = matches
    .map(match => Number(match));

  const latex = polynomial
    .map((c, i) => {
      const coefficient = String(c)
      const x_term = `x^{${i}}`;
    })
    .join(" + ");

  const text = matches.join('');

  return { text, polynomial, latex };
}

export class PolynomialInput extends React.Component< {}, State > {
  constructor(props : {}) {
    super(props);
    this.state = defaultState;
    this.updateText = this.updateText.bind(this);
  }
  
  updateText(event: any) {
    const input = event.target.value as string;
    const state = getState(input);
    this.setState(state);
  }

  render() {
    const { text, latex } = this.state;
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
        <Latex input={latex}/>
        <input 
          style={{
            display: 'inline-block',
          }}
          autoFocus
          type='text'
          value={text}
          onChange={this.updateText}/>
      </div>);
  }
}
