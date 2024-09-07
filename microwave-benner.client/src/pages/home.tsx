import { Button } from "@/components/ui/button"
import { Slider } from "@/components/ui/slider"
import { useState } from "react";
import { secondsToHms } from "@/utils/secondsToHms";

export function Home() {
  const [time, setTime] = useState(0);
  const [textTime, setTextTime] = useState('')
  const [power, setPower] = useState([5])
  const [isRunning, setIsRunning] = useState(false)

  function writeOnVisor(digit: number) {
    setTextTime(state => state + digit)
  }

  function StartHeating(){
    console.log(time)
    console.log(power[0])
    setTime(Number(textTime))
    setIsRunning(true)
    
  }

  return <>
    <div className="border rounded-md w-[900px] h-[500px] mx-auto my-20 flex">

      <div className=" h-full flex-1 p-6">
          <div className="border w-full h-full rounded-md">
            
          </div>
      </div>

      <div className="border-l w-[280px] p-6">
        <div className="border rounded-md w-full h-24 flex items-center justify-center">
          <span className="font-bold text-neutral-500 dark:text-neutral-200 text-4xl">
            {isRunning ? secondsToHms(time) : textTime

            }
            </span>
        </div>

        <div className="grid grid-cols-3 justify-center gap-3 py-4">
          {Array.from({length: 9}).map((_, index)=>
            <Button 
              key={index} 
              size="lg" 
              variant="secondary"
              onClick={()=>writeOnVisor(index+1)}
            >{index+1}</Button>
          )}
          <Button size="lg" variant="secondary" onClick={()=>writeOnVisor(0)}>0</Button>
          <Button size="lg" variant="outline" onClick={StartHeating}>Play</Button>
          <Button size="lg" variant="destructive">Cancel</Button>
        </div>

        <div>
          <span className="text-muted-foreground text-sm">Potência</span>
          <Slider className="mt-2" min={1} max={10} value={power} onValueChange={setPower}/>
        </div>
      </div>
    </div>
  </>
}
