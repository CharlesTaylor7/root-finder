import * as React from 'react';
import { Latex } from './Latex';
import Input from '@material-ui/core/Input';

const defaultState = { text: '', polynomial: [0], latex: '' };
type State = typeof defaultState;

const getState = (input: string) : State => {

  const decimal = /(\d*\.?\d* ?)/g;
      
  const matches = input.match(decimal)
    .filter(match => match !== '' && match !== '.');

  const polynomial = matches
    .map(match => match === ' ' ? 0 : Number(match));

  const latex = polynomial
    .map((c, i) => {
      const coefficient = String(c)
      const x_term = `x^{${i}}`;
      return `${coefficient}${x_term}`;
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
    const placeholder ='1 + 5.3x&sup2; + 3x&sup4;';
    return (
      <div style={{
        textAlign: 'center',
        position: 'absolute',
        marginLeft: '2.54cm',
        marginRight: '2.54cm',
        top: 0,
        right: 0,
        bottom: 0,
        left: 0,
        height: '100px',
      }}>
      <Latex input={latex}/>
      <Input
        style={{
          display: 'inline-block',
        }}
        placeholder='1 + 3x - 7x&sup2; + 3x⁴'

        autoFocus
        type='text'
        value={text}
        onChange={this.updateText}/>
      </div>);
  }
}
