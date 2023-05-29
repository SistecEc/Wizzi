/**
 * Devuelve True si el objeto está vacío
 * @param {any} obj
 */
export function checkEmptyObject(obj: any) {
    return Object.keys(obj).length === 0 && obj.constructor === Object
}

/**
 * Devuelve True si el número es par
 * @param {number} num
 */
export function esPar(num: number) {
    return num % 2 == 0;
}

/**
 * Devuelve True si se puede convertir en JSON
 * @param {string} str
 */
export function isJson(str: string) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

/**
 * Devuelve un nuevo array con el objeto reemplazado en la posición indicada
 * @param arr 
 * @param element 
 * @param replaceAt 
 * @returns array
 */
export function replaceWithoutMutate<T>(arr: T[], element: T, replaceAt: number): T[] {
    return [...arr.slice(0, replaceAt), element, ...arr.slice(replaceAt + 1)]
}

/**
 * Devuelve un nuevo array con el objeto reemplazado en la posición indicada
 * @param arr 
 * @param element 
 * @param insertAt 
 * @returns array
 */
export function insertWithoutMutate<T>(arr: T[], element: T, insertAt: number): T[] {
    return [...arr.slice(0, insertAt), element, ...arr.slice(insertAt)]
}