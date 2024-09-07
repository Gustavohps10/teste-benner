import { ToggleTheme } from "./toggle-theme";

export function Header() {
  return (
    <header className="shadow-md">
      <div className="flex mx-auto max-w-[90rem] items-center py-4 gap-2 justify-between">
        <div>Menu</div>
        <ToggleTheme/>
      </div>
    </header>
  )
}
