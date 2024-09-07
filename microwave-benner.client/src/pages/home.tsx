import { Button } from "@/components/ui/button"
import { Slider } from "@/components/ui/slider"

export function Home() {
  return <>
    <div className="border rounded-md w-[900px] h-[500px] mx-auto my-20 flex">

      <div className=" h-full flex-1 p-6">
          <div className="border w-full h-full rounded-md"></div>
      </div>

      <div className="border-l w-[280px] p-6">
        <div className="border rounded-md w-full h-24"></div>

        <div className="grid grid-cols-3 justify-center gap-3 py-4">
          {Array.from({length: 9}).map((_, index)=>
            <Button key={index} size="lg" variant="secondary">{index+1}</Button>
          )}
          <Button size="lg" variant="secondary">0</Button>
          <Button size="lg" variant="secondary">Play</Button>
          <Button size="lg" variant="secondary">Cancel</Button>
        </div>

        <div>
          <span className="text-muted-foreground text-sm">Potência</span>
          <Slider className="mt-2" min={1} max={10}/>
        </div>
      </div>
    </div>
  </>
}
