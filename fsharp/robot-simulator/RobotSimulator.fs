module RobotSimulator

type Direction = North | East | South | West
type Position = int * int

type Robot = { direction: Direction; position: Position }

let create direction position = { direction = direction; position = position }

let turnLeft robot = 
    match robot.direction with
    | North -> { robot with direction = West }
    | East -> { robot with direction = North }
    | South -> { robot with direction = East }
    | West -> { robot with direction = South }

let turnRight robot = 
    match robot.direction with
    | North -> { robot with direction = East }
    | East -> { robot with direction = South }
    | South -> { robot with direction = West }
    | West -> { robot with direction = North }

let advance (robot: Robot) = 
    let (x, y) = robot.position
    match robot.direction with
    | North -> { robot with position = (x, y + 1) }
    | East -> { robot with position = (x + 1, y) }
    | South -> { robot with position = (x, y - 1) }
    | West -> { robot with position = (x - 1, y) }

let rec private runStep robot = 
    function
    | 'A'::rem -> runStep (advance robot) rem
    | 'L'::rem -> runStep (turnLeft robot) rem
    | 'R'::rem -> runStep (turnRight robot) rem
    | _ -> robot

let instructions instructions' robot = Seq.toList instructions' |> runStep robot