namespace RootFinder

//#nowarn "386"
open System
open System

//https://stackoverflow.com/a/31283508/4875161
[<NoComparison>]
[<CustomEquality>]
type Complex =
  struct
    val real: double
    val imag: double
    new (real, imag) =
      { real = real; imag = imag; }
      then
        if Double.IsNaN real || Double.IsNaN imag
        then failwith "Complex number should not have NaN as a component value."
        else ()
  end

  member inline z.normSquared =
   z.real * z.real + z.imag * z.imag;

  member inline z.norm =
    Math.Sqrt z.normSquared

  member inline z.conjugate =
    Complex (z.real, -z.imag)

  member inline z.phase =
    Math.Atan2 (z.imag, z.real)

  override z.ToString() =
    let sign =
      if Double.IsNaN z.imag || Math.Sign z.imag >= 0
      then '+'
      else '-'
    String.Format ("{0} {2} {1}*i", z.real, Math.Abs z.imag, sign)

  static member op_Explicit (z: Complex) = z
  static member op_Explicit (d: double) = Complex (d, 0.0)
  static member op_Explicit (f: float32) = Complex (double f, 0.0)
  static member op_Explicit (n: int) = Complex (double n, 0.0)

  static member inline zero = Complex (0.0, 0.0)
  static member one = Complex (1.0, 0.0)

  // Unary negation
  static member inline (~-) (z: Complex) =
    Complex (-z.real, -z.imag)

  // Scalar operations
  static member inline (*) (s: double, z: Complex) =
    Complex (s * z.real, s * z.imag)

  static member inline (*) (s: int, z: Complex) =
    double s * z

  static member inline (/) (z: Complex, s: double) =
    Complex (z.real / s, z.imag / s)

  // Field operations
  static member inline (+) (z1: Complex, z2: Complex) =
    Complex (z1.real + z2.real, z1.imag + z2.imag);

  static member inline (-) (z1: Complex, z2: Complex) =
    Complex (z1.real - z2.real, z1.imag - z2.imag);

  static member inline (*) (z1: Complex, z2: Complex) =
    Complex (z1.real * z2.real - z1.imag * z2.imag, z1.real * z2.imag + z1.imag * z2.real);

  static member inline (/) (z1: Complex, z2: Complex) =
    (z1 * z2.conjugate) / z2.normSquared

  interface IEquatable<Complex> with
    member z.Equals w = (z - w).norm <= 1e-5

module Complex =
  // Infix constructor. Looks vaguely like '+ i'.
  let inline (+|) x  y =
    Complex (double x, double y)

  let inline complex d =
//  let inline complex (d: ^T when ^T : (static member op_Explicit: ^T -> double)) =
    d +| 0.0

  let inline polar r theta =
    r * (Math.Cos theta +| Math.Sin theta)

  let inline equalWithin delta (z1: Complex) (z2: Complex) =
    (z1 - z2).norm <= delta
