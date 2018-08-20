// Redo in Fable or Purescript.
// The lack of overloaded operators is extremely painful.

import * as C from './complex'; 
import { Complex, zero, product, divide, divideScalar } from './complex'

type Nullable<T> = T | undefined

type Polynomial = ReadonlyArray<Complex>;

const safeAdd = (c1 : Nullable<Complex> , c2: Nullable<Complex>) => {
  return C.add(c1 || C.zero, c2 || C.zero);  
}

export const add = (p1: Polynomial, p2: Polynomial): Polynomial => {
  const result = [];
  for (let i = 0; i < Math.max(p1.length, p2.length); i++){
    result.push(safeAdd(p1[i], p2[i]));
  }
  return result;
}

export const derivative = (polynomial: Polynomial): Polynomial => {
  const d = []
  for (let i = 1; i < polynomial.length; i++)
  {
    d.push(C.scale(polynomial[i], i));
  }
  return d;
}

const enumerate = (n: number) => {
  const list = []
  for (let i = 0; i < n ; i++){
    list.push(i);
  }
  return list;
}

const roots_of_unity = (n: number) => enumerate(n).map(k => C.polar(1, 2 * Math.PI * k / n));

// x^n - 1
const polynomial_of_unity = (n: number) => {
  const polynomial = []
  polynomial[0] = Complex(-1, 0);
  polynomial[n-1] = Complex(1, 0);
  return polynomial;
}

// Uses Horner's Rule
const evaluateAt = (polynomial: Polynomial, x: Complex) => {
  let total = zero; 
  for (let i = polynomial.length - 1; i >= 0; i--) {
    total = C.add(product(x, total), polynomial[i]); 
  }
  return total;
}

const scale = (polynomial: Polynomial, scalar: number) => polynomial.map(coefficient => C.scale(coefficient, scalar));
const interpolate = (p1: Polynomial, p2: Polynomial, t: number) => add(scale(p1, 1 - t), scale(p2, t));

const newton_solve = (polynomial: Polynomial, guess: Complex, delta: number) => {
  const p = polynomial;
  const d = derivative(polynomial);
  const newton = (x: Complex) => { 
    // x - P(x) / D(x)
    return C.subtract(x, C.divide(evaluateAt(p, x), evaluateAt(d, x)));
  }
  let delta_squared = delta * delta;
  let x_prev;  
  let x_current = guess;
  do {
    x_prev = x_current;
    x_current = newton(x_current);
  } while (C.normSquared(C.subtract(x_prev, x_current)) > delta_squared);
  
  return x_current;
}

const solve = (polynomial: Polynomial) => {
  const n = polynomial.length;
  const roots = roots_of_unity(n);
  const p_start = polynomial_of_unity(n);
  const p_final = polynomial;
  const step_count = 10;
  const delta = 0.01;

  const t = (i: number) => (i + 1) / step_count;

  const reducer = (current: Complex, i: number) => {
    const p = interpolate(p_start, p_final, t(i));
    return newton_solve(p, current, delta);
  }

  return roots.map(root => enumerate(step_count).reduce(reducer, root));
}
