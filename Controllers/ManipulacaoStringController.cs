using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace manipulacao_string.Controllers  {
        [Route("api/[controller]")]
        [ApiController]
        public class ManipulacaoStringController : ControllerBase {
            [HttpPost]
            public IActionResult ManipularString([FromBody] DadosEntrada dados) {
                if (string.IsNullOrEmpty(dados?.Texto)) {
                    return BadRequest("Texto n�o fornecido.");
                }

                // Verifica se a string � um pal�ndromo
                bool palindromo = VerificarPalindromo(dados.Texto);

                // Conta as ocorr�ncias de cada caractere na string
                Dictionary<char, int> ocorrenciasCaracteres = ContarOcorrenciasCaracteres(dados.Texto);

                // Monta o corpo de sa�da
                var resultado = new {
                    palindromo = palindromo,
                    ocorrencias_caracteres = ocorrenciasCaracteres
                };

                // Retorna o resultado em formato JSON
                return Ok(resultado);
            }

            private bool VerificarPalindromo(string texto) {
                // Remove os espa�os em branco e converte para min�sculas
                string textoFormatado = texto.Replace(" ", "").ToLower();
                // Compara o texto original com sua vers�o invertida
                return textoFormatado == new string(textoFormatado.Reverse().ToArray());
            }

            private Dictionary<char, int> ContarOcorrenciasCaracteres(string texto) {
                Dictionary<char, int> ocorrencias = new Dictionary<char, int>();

                foreach (char caractere in texto) {
                    if (!char.IsWhiteSpace(caractere)) // Ignora espa�os em branco
                    {
                        if (ocorrencias.ContainsKey(caractere)) {
                            ocorrencias[caractere]++;
                        }
                        else {
                            ocorrencias.Add(caractere, 1);
                        }
                    }
                }

                return ocorrencias;
            }
        }

        public class DadosEntrada {
            public string Texto { get; set; }
        }
    }
