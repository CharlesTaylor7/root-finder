
interface IComplex {
  real: number,
  imag: number,
}

export type Complex = Readonly<IComplex>;
export const Complex = (real: number, imag: number) => { return { real, imag } };

export const polar = (norm : number, angle: number) => Complex(norm * Math.cos(angle), norm * Math.sin(angle));

export const unit = Complex(1, 0);
export const zero = Complex(0, 0);

export const negate = ({real: x, imag: y} : Complex) => Complex(-x, -y);
export const conjugate = ({real: x, imag: y} : Complex) => Complex(x, -y);

export const normSquared = ({real: x, imag: y} : Complex) => x * x + y * y;
export const norm = (z : Complex) => Math.sqrt(normSquared(z));

export const inverse = (z: Complex) => divideScalar(conjugate(z), normSquared(z));

export const add = ({real: x1, imag: y1}: Complex, {real: x2, imag: y2}: Complex) => Complex(x1 + x2, y1 + y2);
export const subtract = ({real: x1, imag: y1}: Complex, {real: x2, imag: y2}: Complex) => Complex(x1 - x2, y1 - y2);
export const product = ({real: x1, imag: y1}: Complex, {real: x2, imag: y2}: Complex) => Complex(x1 * x2 - y1 * y2, x1 * y2 + x2 * y1);
export const divide = (c1: Complex, c2: Complex) => product(c1, inverse(c2));

export const scale = ({real: x, imag: y}: Complex, scalar: number) => Complex(scalar * x, scalar * y);
export const divideScalar = ({real: x, imag: y}: Complex, divisor: number) => Complex(x / divisor, y / divisor);

