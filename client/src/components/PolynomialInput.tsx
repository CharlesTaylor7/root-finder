import * as React from 'react';
// import { HotKeys } from 'react-hotkeys';

// const keyMap = {
//   nextCoefficient: 'tab',
//   backspace: 'backspace'
// }

// const handlers = {
//   'moveUp': (event) => console.log('Move up hotkey called!')
// };

// <HotKeys keyMap={keyMap} handlers={handlers} />
export class PolynomialInput extends React.Component<{}, { text : string } > {

  constructor(props : {}) {  
    super(props);
      
    this.state  = {
        text : " " 
    };  
      
    this.onChange = this.onChange.bind(this);  
  }  
  
  onChange(e : any) {  
      const textInput = e.target.value;
      

      this.setState({text : textInput});  
  }  
  
  render() {  
      return <input type="text" value={this.state.text} onChange={this.onChange} />  
  }  

}
