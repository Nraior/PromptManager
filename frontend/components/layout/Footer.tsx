import Link from "next/link";
import { FaGithub, FaLinkedin } from "react-icons/fa";
import { AUTHOR, LINKS } from "@/lib/constants";
export default function Footer() {
  return (
    <footer className="bg-gray-900 text-white py-4 text-center mt-auto">
      <p className="mb-2">Prompt Manager, 2026 {AUTHOR}</p>
      <div className="flex items-center justify-center gap-4">
        {LINKS?.github && (
          <Link
            className="hover:text-gray-400"
            rel="noopener noreferrer"
            target="_blank"
            href={LINKS.github}
          >
            <FaGithub size={26} />
          </Link>
        )}
        {LINKS?.linkedin && (
          <Link
            className="hover:text-gray-400"
            target="_blank"
            rel="noopener noreferrer"
            href={LINKS.linkedin}
          >
            <FaLinkedin size={26} />
          </Link>
        )}
      </div>
    </footer>
  );
}
