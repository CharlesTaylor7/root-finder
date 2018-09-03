namespace RootFinder

//#nowarn "386"
open System
open System

//https://stackoverflow.com/a/31283508/4875161
[<NoComparison>]
[<CustomEquality>]
type Complex =
  struct
    val real: float
    val imag: float
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
    String.Format ("{0} {2} {1}*i", Math.Round(z.real, 10), Math.Round(Math.Abs z.imag, 10), sign)

  static member op_Explicit (z: Complex) = z
  static member op_Explicit (d: float) = Complex (d, 0.0)
  static member op_Explicit (f: float32) = Complex (float f, 0.0)
  static member op_Explicit (n: int) = Complex (float n, 0.0)

  static member inline zero = Complex (0.0, 0.0)
  static member one = Complex (1.0, 0.0)

  // Unary negation
  static member inline (~-) (z: Complex) =
    Complex (-z.real, -z.imag)

  // Scalar operations
  static member inline (*) (s: float, z: Complex) =
    Complex (s * z.real, s * z.imag)

  static member inline (*) (s: int, z: Complex) =
    float s * z

  static member inline (/) (z: Complex, s: float) =
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

  static member inline tolerance() = 1e-10

  interface IEquatable<Complex> with
    member z.Equals w =
      (z - w).normSquared <= Complex.tolerance()

[<AutoOpen>]
module Complex =
  // Infix constructor. Looks vaguely like '+ i'.
  let inline (+|) x  y =
    Complex (float x, float y)

  let inline complex d =
    d +| 0.0

  let inline polar r theta =
    r * (Math.Cos theta +| Math.Sin theta)
