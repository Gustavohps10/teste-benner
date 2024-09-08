import { Link } from 'react-router-dom'

import { ToggleTheme } from './toggle-theme'
import { Button } from './ui/button'

export function Header() {
  return (
    <header className="shadow-md">
      <div className="flex mx-auto max-w-[90rem] items-center py-4 gap-2 justify-between px-2">
        <div className="flex">
          <Link to="/">
            <Button variant="link">Home</Button>
          </Link>
          <Link to="/programs">
            <Button variant="link">Programas</Button>
          </Link>
        </div>
        <ToggleTheme />
      </div>
    </header>
  )
}
